import { Routes } from '@angular/router';
import { Feed } from './pages/feed/feed';
import { Discover } from './pages/discover/discover';
import { Ranking } from './pages/ranking/ranking';
import { LatestReleases } from './pages/latest-releases/latest-releases';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Home } from './pages/home/home';
//development
export const routes: Routes = [
  {
    path: '',
    component: Home,
    title: 'Ani Connect'
  },
  {
    path: 'feed',
    component: Feed,
    title: 'Ani Connect - Home'
  },
  {
    path: 'discover',
    component: Discover,
    title: 'Ani Connect - Explorar'
  },
  {
    path: 'ranking',
    component: Ranking,
    title: 'Ani Connect - Melhores'
  },
  {
    path: 'latest-releases',
    component: LatestReleases,
    title: 'Ani Connect - Lançamentos'
  },
  {
    path: 'login',
    component: Login,
    title: 'Ani Connect - Login'
  },
  {
    path: 'register',
    component: Register,
    title: 'Ani Connect - register'
  }
];
