import { Injectable, signal } from '@angular/core';
import { ISession } from '../../intefaces/ISession';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SessionService {

  private readonly storageKey = 'user_session';
  private readonly Token_Key = 'access_token';

  private _session$ = new BehaviorSubject<ISession | null>(null);

  public session$: Observable<ISession | null> = this._session$.asObservable();

  constructor() { }

  public loadFromStorage(): ISession | null {
    const data = localStorage.getItem(this.storageKey);

    this._session$.next(data ? JSON.parse(data) : null)

    return data ? JSON.parse(data) : null;
  }

  setSession(session: ISession)
  {
    localStorage.setItem(this.storageKey, JSON.stringify(session));
    this._session$.next(session);
  }

  clearSession()
  {
    localStorage.removeItem(this.storageKey);
    this._session$.next(null);
  }

  get currentSession(): ISession | null {
    return this._session$.value;
  }

  get isLoggedIn() : boolean
  {
    return this._session$.value !== null;
  }
}
