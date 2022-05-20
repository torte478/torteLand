namespace TorteLand;

public record Article(
    int Id,
    int Parent,
    string Title,
    string Body);