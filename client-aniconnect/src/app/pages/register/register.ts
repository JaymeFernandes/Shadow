import { Component, OnInit, signal } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Eye, EyeOff, LucideAngularModule } from 'lucide-angular';
import { matchPasswords, passwordValidator } from '../../shared/Validators/PasswordValidator';
import { AuthService } from '../../core/auth/services/auth/auth-service';
import { ProblemDetails } from '../../shared/interfaces/IApiResponse';

@Component({
  selector: 'app-register',
  imports: [ LucideAngularModule, RouterLink, ReactiveFormsModule ],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class Register implements OnInit {

  errorMessage = signal<string | null>(null);

  registerForm!: FormGroup;

  viewPassword = signal<boolean>(false);


  get username()
  {
    return this.registerForm.get('username')!;
  }

  get email()
  {
    return this.registerForm.get('email')!;
  }

  get password()
  {
    return this.registerForm.get('password')!;
  }

  get confirmPassword()
  {
    return this.registerForm.get('confirmPassword')!;
  }


  readonly Eye = Eye;
  readonly EyeOff = EyeOff;

  constructor(private route: Router, private auth: AuthService) { }

  ngOnInit(): void {
    this.registerForm = new FormGroup({
      username: new FormControl('', [ Validators.minLength(4), Validators.required ]),
      email: new FormControl('', [ Validators.email, Validators.required ]),
      password: new FormControl('', [  Validators.required, Validators.minLength(8), Validators.maxLength(30), passwordValidator() ]),
      confirmPassword: new FormControl('', [ Validators.required, matchPasswords('password', 'confirmPassword') ])
    });
  }

  submit(){
    if(this.registerForm.invalid)
      return;

    const { username, email, password } = this.registerForm.value;

    this.auth.register(username, username, email, password).subscribe({
      next: () => {
        this.route.navigate(['/login'])
      },
      error: (err : ProblemDetails) => {

        if (err.status === 401) {
          this.errorMessage.set("Erro ao criar o usuário: credenciais inválidas ou já cadastradas.");
        } else if (err.status === 0) {
          this.errorMessage.set("Não foi possível conectar ao servidor. Verifique sua internet.");
        }
        else if (err.status === 400){
          console.log(err.detail)
          this.errorMessage.set(err.detail ?? "Nome/Email já sendo ultilizado")
        }
        else {
          this.errorMessage.set("Ocorreu um erro inesperado. Tente novamente mais tarde.");
        }

      }
    })
  }
}
