import { Ranking } from './../../ranking';
import { Component, Input } from '@angular/core';
import { IRankingUser } from '../../../../shared/interfaces/Ranking/IRankingUser';
import { LucideAngularModule, Trophy, Users, Star } from 'lucide-angular';

@Component({
  selector: 'app-ranking-top',
  imports: [ LucideAngularModule ],
  templateUrl: './ranking-top.html',
  styleUrl: './ranking-top.scss'
})
export class RankingTop {
  @Input({ required: true }) ranking! : IRankingUser;
  @Input({  required: true }) top! : Number;

  readonly Trophy = Trophy;
  readonly Users = Users;
  readonly Star = Star;
}
