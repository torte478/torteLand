import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Article } from '../models/article';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {

  private url = 'api/Article';

  constructor(private http: HttpClient) { }

  getArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(this.url)
      .pipe(
        tap(_ => console.log('success')),
        catchError(this.handleError<Article[]>('getArticles', []))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`(${operation}): ${JSON.stringify(error)}`);
      return of(result as T);
    }
  }
}
