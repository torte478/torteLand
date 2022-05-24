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
        var entity = await _context.Articles.AddAsync(value.Map());
        await _context.SaveChangesAsync();
        return entity.Entity.Map();
    }

    public async Task<Article> ReadAsync(int key)
    {
        var entity = await GetAsync(key);
        return entity.Map();
    }

    public async Task<Article> UpdateAsync(Article value)
    {
        var entity = await GetAsync(value.Id);

        entity.Title = value.Title;
        entity.Body = value.Body;
        await _context.SaveChangesAsync();

        return entity.Map();
    }

    public async Task<Article> DeleteAsync(int key)
    {
        var entity = await GetAsync(key);
        _context.Articles.Remove(entity);
        await _context.SaveChangesAsync();
        return entity.Map();
    }

    public async IAsyncEnumerable<Article> ToAsyncEnumerable()
    {
        await foreach (var entity in _context.Articles)
            yield return entity.Map();
    }

    private async Task<DbArticle> GetAsync(int id)
    {
        var entity = await _context.Articles.FindAsync(id);
        if (entity is null)
            throw new Exception($"entity with key {id} is null");

        return entity;
    }
}