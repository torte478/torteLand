namespace TorteLand;

public sealed class Articles : IArticles
{
    private readonly IAcrud<int, Article> _acrud;

    public Articles(IAcrud<int, Article> acrud)
    {
        _acrud = acrud;
    }

    public IAsyncEnumerable<Article> AllAsync()
    {
        return _acrud.AllAsync();
    }

    public Task<Article> CreateAsync(Article value)
    {
        return _acrud.CreateAsync(value);
    }

    public Task<Article> ReadAsync(int key)
    {
        return _acrud.ReadAsync(key);
    }

    public Task<Article> UpdateAsync(Article value)
    {
        return _acrud.UpdateAsync(value);
    }

    public Task<Article> DeleteAsync(int key)
    {
        return _acrud.DeleteAsync(key);
    }
}