import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { Article } from '../models/article';

@Injectable({
  providedIn: 'root'
})
export class ArticlesService {

  private url = 'api/Article';

  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };

  constructor(private http: HttpClient) { }

  all(): Observable<Article[]> {
    return this.http.get<Article[]>(this.url)
      .pipe(
        catchError(this.handleError<Article[]>('all', []))
      );
  }

  read(id: number): Observable<Article> {
    const url = `${this.url}/${id}`;
    return this.http.get<Article>(url)
      .pipe(
        catchError(this.handleError<Article>('read'))
      );
  }

  create(article: Article): Observable<Article> {
    return this.http.post<Article>(this.url, article, this.httpOptions)
      .pipe(
        catchError(this.handleError<Article>('create'))
      );
  }

  update(article: Article): Observable<Article> {
    return this.http.put<Article>(this.url, article, this.httpOptions)
      .pipe(
        catchError(this.handleError<Article>('update'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`(${operation}): ${JSON.stringify(error)}`);
      return of(result as T);
    }
  }
}
