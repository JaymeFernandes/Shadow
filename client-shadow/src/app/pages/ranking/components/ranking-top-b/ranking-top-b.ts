import { Component, Input } from '@angular/core';
import { IRankingUser } from '../../../../shared/interfaces/Ranking/IRankingUser';
import { LucideAngularModule, Trophy, Star, UserRound } from 'lucide-angular';

@Component({
  selector: 'app-ranking-top-b',
  imports: [ LucideAngularModule ],
  templateUrl: './ranking-top-b.html',
  styleUrl: './ranking-top-b.scss'
})
export class RankingTopB {
  @Input({ required: true }) top! : Number;
  @Input( { required: true} ) ranking! : IRankingUser;

  readonly Trophy = Trophy;
  readonly Star = Star;
}
