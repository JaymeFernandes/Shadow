import { Component } from '@angular/core';

import { House, Trophy, Telescope, BookMarked, UserRound, Settings, Bot, CalendarDays, Crown, CircleHelp } from 'lucide-angular';
import { IMenuOption } from '../../../../shared/interfaces/IMenuOptions';
import { SideBarMenuOptions } from './components/side-bar-menu-options/side-bar-menu-options';



@Component({
  selector: 'app-side-bar',
  standalone: true,
  imports: [ SideBarMenuOptions ],
  templateUrl: './side-bar.html',
  styleUrl: './side-bar.scss'
})
export class SideBar {
  readonly navItems : IMenuOption[] = [
    {
      name: 'Início',
      index: '',
      icon: House,
      exact: true
    },
    {
      name: 'Explorar',
      index: 'discover',
      icon: Telescope,
      exact: false
    },
    {
      name: 'Tendências',
      index: 'ranking',
      icon: Trophy,
      exact: false
    },
    {
      name: 'Lançamentos',
      index: 'new',
      icon: CalendarDays,
      exact: false
    }
  ];

  readonly userItems: IMenuOption[] =
  [
    {
      name: 'Perfil',
      index: 'user',
      icon: UserRound,
      exact: true
    },
    {
      name: 'Minha Lista',
      index: 'bookmark',
      icon: BookMarked,
      exact: false
    },
    {
      name: 'Conquistas',
      index: 'user/badges',
      icon: Crown,
      exact: true
    },
    {
      name: 'Configurações',
      index: 'settings',
      icon: Settings,
      exact: false
    }
  ]

  readonly outItems : IMenuOption[] =
  [
    {
      name: 'Desenvolvedores',
      index: 'development',
      icon: Bot,
      exact: false
    },
    {
      name: 'Ajuda',
      index: 'help',
      icon: CircleHelp,
      exact: false
    }
  ]
}
