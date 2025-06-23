import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-ranking-top-placehoder',
  imports: [],
  templateUrl: './ranking-top-placehoder.html',
  styleUrl: './ranking-top-placehoder.scss'
})
export class RankingTopPlacehoder {
  @Input({  required: true }) top! : Number;
}
