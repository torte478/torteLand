using DbArticle = TorteLand.PostgreSql.Models.Article;

namespace TorteLand.PostgreSql;

internal static class Mapping
{
    public static Article Map(this DbArticle origin)
        => new(
            origin.Id,
            origin.Title,
            origin.Body);

    public static DbArticle Map(this Article origin)
        => new()
           {
               Id = origin.Id,
               Title = origin.Title,
               Body = origin.Body
           };
}