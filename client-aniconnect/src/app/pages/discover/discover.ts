import { Component, inject, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-discover',
  imports: [],
  templateUrl: './discover.html',
  styleUrl: './discover.scss'
})
export class Discover {
  private http = inject(HttpClient);
  data = signal<Temp | undefined>(undefined);

  constructor() {
    this.http.get<Temp>('https://jsonplaceholder.typicode.com/posts/1')
      .subscribe(result => {
        this.data.set(result);
      });
  }

}

interface Temp
{
  userId: Number;
  id: Number;
  title: String;
  body: String;
}
