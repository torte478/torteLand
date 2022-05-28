using Microsoft.EntityFrameworkCore;
using TorteLand.PostgreSql.Models;

using DbArticle = TorteLand.PostgreSql.Models.Article;

namespace TorteLand.PostgreSql;

public sealed class Articles : ICrudl<int, Article>
{
    private readonly Context _context;

    public Articles(Context context)
    {
        _context = context;
    }

    public async Task<Article> CreateAsync(Article value)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        
        var body = new ArticleBody { Body = value.Body };
        body = (await _context.AddAsync(body)).Entity;
        await _context.SaveChangesAsync();

        var article = new DbArticle { Title = value.Title, BodyId = body.Id };
        article = (await _context.Articles.AddAsync(article)).Entity;
        await _context.SaveChangesAsync();

        await transaction.CommitAsync();

        return article.Map();
    }

    public async Task<Article> ReadAsync(int key)
    {
        var (article, body) = await ToArticleAsync(key);

        return new Article(article.Id, article.Title, body.Body);
    }

    public async Task<Article> UpdateAsync(Article value)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        
        var (article, body) = await ToArticleAsync(value.Id);

        article.Title = value.Title;
        body.Body = value.Body;

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return article.Map();
    }

    public async Task<Article> DeleteAsync(int key)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        
        var (article, body) = await ToArticleAsync(key);
        _context.Articles.Remove(article);
        _context.ArticleBodies.Remove(body);

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return article.Map();
    }

    public async IAsyncEnumerable<Article> ToAsyncEnumerable()
    {
        await foreach (var entity in _context.Articles)
            yield return entity.Map();
    }

    private async Task<(DbArticle, ArticleBody)> ToArticleAsync(int id)
    {
        var article = await ToEntity(_context.Articles, id);
        var body = await ToEntity(_context.ArticleBodies, article.BodyId);
        return (article, body);
    }
    
    private static async Task<T> ToEntity<T>(DbSet<T> table, int id) where T : class
    {
        var entity = await table.FindAsync(id);
        
        if (entity is null)
            throw new Exception($"entity with key {id} is null");

        return entity;
    }
}