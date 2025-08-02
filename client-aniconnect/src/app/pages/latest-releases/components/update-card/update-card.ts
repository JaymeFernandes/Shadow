import { Component, Input } from '@angular/core';
import { IRelease } from '../../../../shared/interfaces/Lastest-Releases/IRelease';

@Component({
  selector: 'app-update-card',
  imports: [],
  templateUrl: './update-card.html',
  styleUrl: './update-card.scss'
})
export class UpdateCard {
  @Input({ required: true }) update! : IRelease;
}
