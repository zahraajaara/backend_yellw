using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/BulkSms
    public class BulkSmsController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public BulkSmsController(YellowKalamContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BulkSm>>> GetBulkSms()
        {
            var items = await _context.BulkSms
                .AsNoTracking()
                .OrderByDescending(b => b.LastUpdated)
                .ToListAsync();

            return Ok(items);
        }
    }
}
