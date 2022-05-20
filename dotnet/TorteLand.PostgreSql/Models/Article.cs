namespace TorteLand.PostgreSql.Models
{
    //TODO: to internal
    public partial class Article
    {
        public Article()
        {
            InverseParent = new HashSet<Article>();
        }

        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public virtual Article Parent { get; set; }
        public virtual ICollection<Article> InverseParent { get; set; }
    }
}
