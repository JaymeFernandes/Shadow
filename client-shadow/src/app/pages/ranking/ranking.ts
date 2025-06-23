import { Component, signal } from '@angular/core';
import { LucideAngularModule, Trophy, Users, Star } from 'lucide-angular';
import { IRankingUser } from '../../shared/interfaces/Ranking/IRankingUser';
import { RankingTop } from './components/ranking-top/ranking-top';
import { RankingTopB } from './components/ranking-top-b/ranking-top-b';
import { RankingTopPlacehoder } from './components/ranking-top-placehoder/ranking-top-placehoder';
import { RankingTopBPlacehoder } from './components/ranking-top-b-placehoder/ranking-top-b-placehoder';

@Component({
  selector: 'app-ranking',
  imports: [ LucideAngularModule, RankingTop, RankingTopB, RankingTopPlacehoder, RankingTopBPlacehoder ],
  templateUrl: './ranking.html',
  styleUrl: './ranking.scss'
})
export class Ranking {
  readonly Trophy = Trophy;
  readonly Users = Users;

  isLoading = signal<Boolean>(false)

  readonly ranking: IRankingUser[] = [
    {
      user: {
        nick: '@idzmon',
        name: 'Idzmon',
        avatar: 'https://media.tenor.com/1uKpKHCxNsEAAAAi/anime-joget-rgb.gif'
      },
      nivel: 100,
      trophy: 235,
      exp: 6000
    },
    {
      user: {
        nick: '@rimuru',
        name: 'I.T.A.C.H.I',
        avatar: 'https://media.tenor.com/VlcpKOUOq8gAAAAj/one-piece-nico-robin.gif'
      },
      nivel: 99,
      trophy: 200,
      exp: 6000
    },
    {
      user: {
        nick: '@lua',
        name: 'Yasmin',
        avatar: 'https://media.tenor.com/AjfRZZrv_uMAAAAi/cute-anime.gif'
      },
      nivel: 70,
      trophy: 150,
      exp: 6000
    },
    {
      user: {
        nick: '@akari',
        name: 'Akari',
        avatar: 'https://media1.tenor.com/m/QdgD_hOiy04AAAAC/hinata-hoshino-anime.gif'
      },
      nivel: 68,
      trophy: 140,
      exp: 5500
    },
    {
      user: {
        nick: '@kitsune',
        name: 'Aiko',
        avatar: 'https://media.tenor.com/XLSMqU7OZWAAAAAi/fox-fox-girl.gif'
      },
      nivel: 65,
      trophy: 130,
      exp: 5300
    },
    {
      user: {
        nick: '@kuro',
        name: 'Kuro',
        avatar: 'https://media1.tenor.com/m/v-AaA404W1gAAAAd/anime-triste-rain.gif'
      },
      nivel: 60,
      trophy: 120,
      exp: 5000
    },
    {
      user: {
        nick: '@shiro',
        name: 'Shiro',
        avatar: 'https://media1.tenor.com/m/sEx6g369yO0AAAAC/hd-triste.gif'
      },
      nivel: 55,
      trophy: 100,
      exp: 4500
    },
    {
      user: {
        nick: '@miyu',
        name: 'Miyu',
        avatar: 'https://media1.tenor.com/m/bQLpUoAOarUAAAAd/anime-girl-smile-cute-anime-girl.gif'
      },
      nivel: 52,
      trophy: 90,
      exp: 4200
    }
  ];

}
