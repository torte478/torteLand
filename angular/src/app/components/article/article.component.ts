import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Article } from 'src/app/models/article';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {

  article: Article | undefined;
  isEdit: Boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private articleService: ArticlesService) { }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.articleService.read(id)
      .subscribe(article => this.article = article);
  }

  startEdit(): void {
    this.isEdit = true;
  }

  onEditCancel(): void {
    this.isEdit = false;
  }

  onSave(): void {
    this.articleService.update(this.article!)
      .subscribe(x => {
        this.article = x;
        this.isEdit = false;
      });
  }

  onDelete(): void {
    if (!confirm('Delete this article?'))
      return;

    this.articleService.delete(this.article!)
      .subscribe(_ => this.router.navigate(['/articles']));
  }
}
