import { Component } from '@angular/core';
import { LucideAngularModule, Calendar, Search, Funnel, Bookmark } from 'lucide-angular';
import { IUpdateDay } from '../../shared/interfaces/Lastest-Releases/IRelease';
import { UpdateCard } from './components/update-card/update-card';

@Component({
  selector: 'app-latest-releases',
  imports: [ LucideAngularModule, UpdateCard ],
  templateUrl: './latest-releases.html',
  styleUrl: './latest-releases.scss'
})
export class LatestReleases {
  readonly Calendar = Calendar;
  readonly Search = Search;
  readonly Funnel = Funnel;
  readonly Bookmark = Bookmark;

  readonly updates : IUpdateDay[] = [
    {
      title: 'Hoje',
      updates:
      [
        {
          title: 'Naruto',
          cover: 'https://occ-0-8407-90.1.nflxso.net/dnm/api/v6/Z-WHgqd_TeJxSuha8aZ5WpyLcX8/AAAABdIFYfkylotuQsosH1WlGXHoouJSR59IKXL-xHz1EBGnDra3h7PiJ0E7Oe8wZzKk0hpHSKQGHi7VrGSs64kozAD0u9dXXKO-T4SM.jpg?r=33e',
          isNew: true,
          cap: 502
        },
        {
          title: 'Naruto',
          cover: 'https://occ-0-8407-90.1.nflxso.net/dnm/api/v6/Z-WHgqd_TeJxSuha8aZ5WpyLcX8/AAAABdIFYfkylotuQsosH1WlGXHoouJSR59IKXL-xHz1EBGnDra3h7PiJ0E7Oe8wZzKk0hpHSKQGHi7VrGSs64kozAD0u9dXXKO-T4SM.jpg?r=33e',
          isNew: true,
          cap: 502
        },
        {
          title: 'Naruto',
          cover: 'https://occ-0-8407-90.1.nflxso.net/dnm/api/v6/Z-WHgqd_TeJxSuha8aZ5WpyLcX8/AAAABdIFYfkylotuQsosH1WlGXHoouJSR59IKXL-xHz1EBGnDra3h7PiJ0E7Oe8wZzKk0hpHSKQGHi7VrGSs64kozAD0u9dXXKO-T4SM.jpg?r=33e',
          isNew: true,
          cap: 502
        },
        {
          title: 'Naruto',
          cover: 'https://occ-0-8407-90.1.nflxso.net/dnm/api/v6/Z-WHgqd_TeJxSuha8aZ5WpyLcX8/AAAABdIFYfkylotuQsosH1WlGXHoouJSR59IKXL-xHz1EBGnDra3h7PiJ0E7Oe8wZzKk0hpHSKQGHi7VrGSs64kozAD0u9dXXKO-T4SM.jpg?r=33e',
          isNew: true,
          cap: 502
        }
      ]
    },
    {
      title: 'Ontem',
      updates: [
        {
          title: 'Naruto',
          cover: 'https://occ-0-8407-90.1.nflxso.net/dnm/api/v6/Z-WHgqd_TeJxSuha8aZ5WpyLcX8/AAAABdIFYfkylotuQsosH1WlGXHoouJSR59IKXL-xHz1EBGnDra3h7PiJ0E7Oe8wZzKk0hpHSKQGHi7VrGSs64kozAD0u9dXXKO-T4SM.jpg?r=33e',
          isNew: true,
          cap: 502
        }
      ]
    },
    {
      title: '3 Dias',
      updates: [
        {
          title: 'Naruto',
          cover: 'https://occ-0-8407-90.1.nflxso.net/dnm/api/v6/Z-WHgqd_TeJxSuha8aZ5WpyLcX8/AAAABdIFYfkylotuQsosH1WlGXHoouJSR59IKXL-xHz1EBGnDra3h7PiJ0E7Oe8wZzKk0hpHSKQGHi7VrGSs64kozAD0u9dXXKO-T4SM.jpg?r=33e',
          isNew: true,
          cap: 502
        },
        {
          title: 'Naruto',
          cover: 'https://occ-0-8407-90.1.nflxso.net/dnm/api/v6/Z-WHgqd_TeJxSuha8aZ5WpyLcX8/AAAABdIFYfkylotuQsosH1WlGXHoouJSR59IKXL-xHz1EBGnDra3h7PiJ0E7Oe8wZzKk0hpHSKQGHi7VrGSs64kozAD0u9dXXKO-T4SM.jpg?r=33e',
          isNew: true,
          cap: 502
        }
      ]
    }
  ]
}
