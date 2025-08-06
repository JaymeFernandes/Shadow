import { Router } from '@angular/router';
import { SessionService } from './../../core/auth/services/session/session-service';
import { afterNextRender, afterRenderEffect, Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home {
  constructor(private session: SessionService, private route : Router)
  {

  }
}
