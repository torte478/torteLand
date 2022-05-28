namespace TorteLand.PostgreSql.Models;

public partial class ArticleBody
{
    public ArticleBody()
    {
        Articles = new HashSet<Article>();
    }

    public int Id { get; set; }
    public string Body { get; set; }

    public virtual ICollection<Article> Articles { get; set; }
}