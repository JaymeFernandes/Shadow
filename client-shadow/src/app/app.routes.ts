import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { Home } from './pages/home/home';
import { Discover } from './pages/discover/discover';
import { Ranking } from './pages/ranking/ranking';
import { LatestReleases } from './pages/latest-releases/latest-releases';
//development
export const routes: Routes = [
  {
    path: '',
    component: Home,
    title: 'Shadow - Home'
  },
  {
    path: 'discover',
    component: Discover,
    title: 'Shadow - Explorar'
  },
  {
    path: 'ranking',
    component: Ranking,
    title: 'Shadow - Melhores'
  },
  {
    path: 'latest-releases',
    component: LatestReleases,
    title: 'Shadow - Lançamentos'
  }
];
