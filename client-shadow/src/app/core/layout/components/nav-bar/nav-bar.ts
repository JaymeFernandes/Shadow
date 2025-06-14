import { Component } from '@angular/core';
import { LucideAngularModule, Search, Bell, ChevronDown } from 'lucide-angular';


@Component({
  selector: 'app-nav-bar',
  imports: [ LucideAngularModule ],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.scss'
})
export class NavBar {
  readonly Search = Search;
  readonly Bell = Bell;
  readonly ChevronDown = ChevronDown;
}
