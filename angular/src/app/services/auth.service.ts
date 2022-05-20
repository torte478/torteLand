import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';

import { Credentials } from '../models/credentials';
import { JwtToken } from '../models/jwtToken';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private url = 'api/Auth/login';

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  private jwtToken: string = '';

  constructor(private http: HttpClient) { }

  login(credentials: Credentials): Observable<boolean> {
    return this.http.post<JwtToken>(this.url, credentials, this.httpOptions)
      .pipe(
        tap(x => this.jwtToken = x.token),
        map(x => true),
        catchError(this.handleError<boolean>('login', false)));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`(${operation}): ${JSON.stringify(error)}`);
      return of(result as T);
    }
  }
}
