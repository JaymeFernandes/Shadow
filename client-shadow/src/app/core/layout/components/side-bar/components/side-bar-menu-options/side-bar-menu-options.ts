import { Component, Input } from '@angular/core';
import { IMenuOption } from '../../../../../../shared/interfaces/IMenuOptions';
import { Router, RouterModule } from '@angular/router';
import { LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-side-bar-menu-options',
  standalone: true,
  imports: [ RouterModule, LucideAngularModule ],
  templateUrl: './side-bar-menu-options.html',
  styleUrl: './side-bar-menu-options.scss'
})
export class SideBarMenuOptions {

  @Input() includeTitle : boolean = false;
  @Input({required: true}) title! : string;
  @Input({required: true}) options! : IMenuOption[];

  constructor(private router: Router) {}

  isActive(url: string, exact: boolean): boolean {

    if(url === '')
    {
      const currentUrl = this.router.url;

      return currentUrl === '/'
    }

    return this.router.isActive(url, {
      paths: exact ? 'exact' : 'subset',
      queryParams: 'ignored',
      fragment: 'ignored',
      matrixParams: 'ignored'
    });
  }
}
