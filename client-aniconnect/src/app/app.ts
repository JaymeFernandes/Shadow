import { AuthService } from './core/auth/services/auth/auth-service';
import { afterNextRender, Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Layout } from './core/layout/layout';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Layout],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  isLoading = signal<boolean>(false);

  protected title = 'client-shadow';

  constructor(private AuthService: AuthService) {
    afterNextRender(() => {
      AuthService.initSession();
      this.isLoading.set(true);
    });
  }
}
