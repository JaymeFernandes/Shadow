import { ISession } from './../../intefaces/ISession';
import { SessionService } from './../session/session-service';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../../environments/environment';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, tap, throwError } from 'rxjs';
import { ApiResponse, ProblemDetails } from '../../../../shared/interfaces/IApiResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly urlBase = environment.apiUrl;

  private readonly Token_Key = 'access_token';
  private readonly Refresh_Key = 'refresh_token';


  constructor(private http: HttpClient, private route: Router, private session: SessionService) { }

  register(username: string, nickname: string, email: string, password: string) : Observable<ApiResponse<any> | ProblemDetails>
  {
    const body = {
      display: username,
      name: nickname,
      email: email,
      password: password
    }

    return this.http.post<ApiResponse<any> | ProblemDetails>(`${this.urlBase}connect/register`, body, {
      headers: { 'Content-Type': 'application/json' }
    }).pipe(
      catchError((error: HttpErrorResponse) => {
        const problem = error.error as ProblemDetails;
        return throwError(() => problem);
      })
    );
  }

  login(username: string, password:string, remenberMe: boolean) : Observable<void>
  {
    const scope = remenberMe ? 'api-shadow offline_access' : 'api-shadow'

    const body = new HttpParams()
      .set('grant_type', 'password')
      .set('client_id', 'api-shadow')
      .set('scope', 'api-shadow offline_access')
      .set('username', username)
      .set('password', password);

    return this.http.post<any>(`${this.urlBase}connect/token`, body, {
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
    }).pipe<any>(
      tap(response => {
        localStorage.setItem(this.Token_Key, response.access_token);
        localStorage.setItem(this.Refresh_Key, response.refresh_token)

        this.setSession();
      })
    );
  }

  refresh() : Observable<void>
  {
    var token = this.refreshToken;

    if(!token)
      return throwError(() => new Error("Refresh token não encontrado"))


    const body = new HttpParams()
      .set('grant_type', 'refresh_token')
      .set('refresh_token', token)
      .set('client_id', 'api-shadow')
      .set('scope', 'api-shadow offline_access');

    return this.http.post<any>(`${this.urlBase}connect/token`, body, {
      headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
    }).pipe<any>(
      tap(response => {
        localStorage.setItem(this.Token_Key, response.access_token);
        localStorage.setItem(this.Refresh_Key, response.refresh_token)


        this.setSession();
      })
    );
  }

  logout(){
    sessionStorage.removeItem(this.Token_Key);
    localStorage.removeItem(this.Refresh_Key);

    this.route.navigate(['/login'])
  }

  get token() : string | null{
    return localStorage.getItem(this.Token_Key);
  }

  get refreshToken() : string | null{
    return localStorage.getItem(this.Refresh_Key);
  }

  private setSession()
  {
    var token = this.token;

    var session = this.http.get<ISession>(`${this.urlBase}api/users/me`, {
      headers: { 'Authorization': `Bearer ${token}` }
    })

    session.pipe(
      tap((x) => this.session.setSession(x))
    ).subscribe();
  }
}
