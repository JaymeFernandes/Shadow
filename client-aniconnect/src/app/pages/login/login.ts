import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Component, signal, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { LucideAngularModule, Eye, EyeOff } from 'lucide-angular';
import { AuthService } from '../../core/auth/services/auth/auth-service';
import { passwordValidator } from '../../shared/Validators/PasswordValidator';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ LucideAngularModule, RouterLink, ReactiveFormsModule ],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class Login implements OnInit {

  errorMessage = signal<string | null>(null);



  loginForm!: FormGroup

  viewPassword = signal<boolean>(true);

  readonly Eye = Eye;
  readonly EyeOff = EyeOff;

  constructor(private route: Router, private auth: AuthService) { }


  get username(){
    return this.loginForm.get('username')!;
  }

  get password(){
    return this.loginForm.get('password')!;
  }


  ngOnInit(): void {
    this.loginForm = new FormGroup({
      username: new FormControl('', [ Validators.required, Validators.minLength(4) ]),
      password: new FormControl('', [ Validators.required, Validators.minLength(8), Validators.maxLength(30), passwordValidator() ]),
      rememberMe: new FormControl(true)
    });
  }

  submit(){
    if(this.loginForm.invalid)
      return;

    const { username, password, rememberMe } = this.loginForm.value;

    this.auth.login(username, password, rememberMe).subscribe({
      next: () => {
        this.route.navigate(['/feed']);
      },
      error: (err) => {
        if(err.status === 401)
          this.errorMessage.set('Usuário ou senha incorretos.')
        else
          this.errorMessage.set('Erro ao tentar fazer login. Tente novamente.');
      }
    })
  }
}
