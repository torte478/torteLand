import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router } from '@angular/router';

import { Article } from 'src/app/models/article';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-article-create',
  templateUrl: './article-create.component.html',
  styleUrls: ['./article-create.component.css']
})
export class ArticleCreateComponent implements OnInit {

  article = new Article(0, '', '');

  constructor(
    private router: Router,
    private location: Location,
    private articleService: ArticlesService
    ) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.articleService.create(this.article)
      .subscribe(_ => this.router.navigate(['/articles']));
  }

  goBack() {
    this.location.back();
  }
}
