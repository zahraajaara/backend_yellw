using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KashefController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public KashefController(YellowKalamContext context)
        {
            _context = context;
        }

        // GET: api/Kashef
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kashef>>> GetKashef()
        {
            // لو حابب ترتّبهم
            return await _context.Kashef
                                 .AsNoTracking()
                                 .OrderByDescending(k => k.ReportDate)
                                 .ToListAsync();
        }
    }
}
