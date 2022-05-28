using TorteLand.PostgreSql.Models;
using DbArticle = TorteLand.PostgreSql.Models.Article;

namespace TorteLand.PostgreSql;

internal static class Mapping
{
    public static Article Map(this DbArticle origin)
        => new(
            origin.Id,
            origin.Title,
            string.Empty);

    public static Article Map(this (DbArticle article, ArticleBody body) _)
        => new(
            _.article.Id,
            _.article.Title,
            _.body.Body);
}