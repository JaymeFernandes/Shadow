import { Component, OnInit, signal, effect } from '@angular/core';
import { SideBar } from "./components/side-bar/side-bar";



@Component({
  selector: 'app-layout',
  imports: [SideBar],
  templateUrl: './layout.html',
  styleUrl: './layout.scss'
})
export class Layout {

}
