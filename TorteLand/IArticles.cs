namespace TorteLand;

public interface IArticles : ICrudl<int, Article>
{
    Task<Article[]> ToChildrenAsync(int id);
}