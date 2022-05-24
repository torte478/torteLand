using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TorteLand.WebAPI2.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public sealed class ArticleController : ControllerBase
{
    private readonly IArticles _articles;

    public ArticleController(IArticles articles)
    {
        _articles = articles;
    }

    [HttpGet]
    public async Task<ActionResult<Article[]>> Get()
        => Ok(await _articles.ToAsyncEnumerable().ToArrayAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> Get(int id)
        => Ok(await _articles.ReadAsync(id));

    [HttpPost]
    public async Task<ActionResult<Article>> Add(Article article)
        => Ok(await _articles.CreateAsync(article));

    [HttpPut]
    public async Task<ActionResult<Article>> Update(Article article)
        => Ok(await _articles.UpdateAsync(article));

    [HttpDelete]
    public async Task<ActionResult<Article>> Delete(int id)
        => Ok(await _articles.DeleteAsync(id));
}