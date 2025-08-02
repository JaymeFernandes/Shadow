import { afterNextRender, Component, effect, signal } from '@angular/core';


import { House, Trophy, Telescope, BookMarked, UserRound, Settings, Bot, Crown, CircleHelp } from 'lucide-angular';
import { IMenuOption } from '../../../../shared/interfaces/Menu/IMenuOptions';
import { SideBarMenuOptions } from './components/side-bar-menu-options/side-bar-menu-options';
import { SideBarMenuOptionMobile } from './components/side-bar-menu-option-mobile/side-bar-menu-option-mobile';
import { SessionService } from '../../../auth/services/session/session-service';



@Component({
  selector: 'app-side-bar',
  standalone: true,
  imports: [ SideBarMenuOptions, SideBarMenuOptionMobile ],
  templateUrl: './side-bar.html',
  styleUrl: './side-bar.scss'
})
export class SideBar {

  isLoggedIn = signal<boolean>(false);

  constructor(private session: SessionService)
  {
    session.session$.subscribe({
      next: (x) => {
        this.isLoggedIn.set(x !== null);
      }
    });

    afterNextRender(() => {
      session.loadFromStorage();
    })
  }


  readonly navItems : IMenuOption[] =
  [
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
      name: 'Ranking',
      index: 'ranking',
      icon: Trophy,
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
      name: 'Configurações',
      index: 'settings',
      icon: Settings,
      exact: false
    }
  ]

  readonly guestItems: IMenuOption[] = [
    { name: 'Início', index: '', icon: House, exact: true },
    { name: 'Explorar', index: 'discover', icon: Telescope, exact: false },
    { name: 'Ranking', index: 'ranking', icon: Trophy, exact: false }
  ];

  readonly guestSessionItems: IMenuOption[] = [
    { name: 'Entrar', index: 'login', icon: UserRound, exact: false },
    { name: 'Cadastrar-se', index: 'register', icon: Crown, exact: false },
    { name: 'Ajuda', index: 'help', icon: CircleHelp, exact: false }
  ];


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

  readonly NaviItemsMobile: IMenuOption[] =
  [
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
      name: 'Minha Lista',
      index: 'bookmark',
      icon: BookMarked,
      exact: false
    },
    {
      name: 'Ranking',
      index: 'ranking',
      icon: Trophy,
      exact: false
    },
    {
      name: 'Perfil',
      index: 'user',
      icon: UserRound,
      exact: true
    }
  ]
}


