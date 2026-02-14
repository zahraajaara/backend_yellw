using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/Articles
    public class ArticlesController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public ArticlesController(YellowKalamContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            var articles = await _context.Articles
                .AsNoTracking()
                .OrderBy(a => a.ArticleId)
                .ToListAsync();

            return Ok(articles);
        }
    }
}
