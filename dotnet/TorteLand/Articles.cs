namespace TorteLand;

public sealed class Articles : IArticles
{
    private readonly ICrudl<int, Article> _crudl;

    public Articles(ICrudl<int, Article> crudl)
    {
        _crudl = crudl;
    }

    public Task<Article> CreateAsync(Article value)
    {
        return _crudl.CreateAsync(value);
    }

    public Task<Article> ReadAsync(int key)
    {
        return _crudl.ReadAsync(key);
    }

    public Task<Article> UpdateAsync(Article value)
    {
        return _crudl.UpdateAsync(value);
    }

    public Task<Article> DeleteAsync(int key)
    {
        return _crudl.DeleteAsync(key);
    }

    public IAsyncEnumerable<Article> ToAsyncEnumerable()
    {
        return _crudl.ToAsyncEnumerable();
    }

    public async Task<Article[]> ToChildrenAsync(int id)
    {
        return await _crudl
               .ToAsyncEnumerable()
               .Where(x => x.Parent == id)
               .ToArrayAsync();
    }
}