import { Component } from '@angular/core';
import { NavBar } from './components/nav-bar/nav-bar';
import { SideBar } from "./components/side-bar/side-bar";

@Component({
  selector: 'app-layout',
  imports: [NavBar, SideBar],
  templateUrl: './layout.html',
  styleUrl: './layout.scss'
})
export class Layout {

}
