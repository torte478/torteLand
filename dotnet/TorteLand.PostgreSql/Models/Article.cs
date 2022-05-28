namespace TorteLand.PostgreSql.Models
{
    //TODO: to internal
    public partial class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int BodyId { get; set; }

        public virtual ArticleBody Body { get; set; }
    }
}
