import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ArticleComponent } from './components/article/article.component';
import { ArticleCreateComponent } from './components/article-create/article-create.component';
import { ArticlesComponent } from './components/articles/articles.component';
import { LoginFormComponent } from './components/login-form/login-form.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginFormComponent },
  { path: 'articles', component: ArticlesComponent },
  { path: 'article/:id', component: ArticleComponent },
  { path: 'articles/create', component: ArticleCreateComponent },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
