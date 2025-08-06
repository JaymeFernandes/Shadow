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


  setSession(session: ISession)
  {
    this._session$.next(session);
  }

  clearSession()
  {
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
