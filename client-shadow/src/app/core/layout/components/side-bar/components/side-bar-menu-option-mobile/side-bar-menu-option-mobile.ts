import { Router, RouterLink } from '@angular/router';
import { Component } from '@angular/core';
import { IMenuOption } from '../../../../../../shared/interfaces/Menu/IMenuOptions';
import { Input } from '@angular/core';
import { LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-side-bar-menu-option-mobile',
  imports: [ LucideAngularModule, RouterLink ],
  templateUrl: './side-bar-menu-option-mobile.html',
  styleUrl: './side-bar-menu-option-mobile.scss'
})
export class SideBarMenuOptionMobile {
  @Input({required: true}) option! : IMenuOption;

  constructor(private router: Router) { }

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
