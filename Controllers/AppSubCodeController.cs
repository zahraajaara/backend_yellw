using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/AppSubCode
    public class AppSubCodeController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public AppSubCodeController(YellowKalamContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppSubCode>>> GetAppSubCodes()
        {
            var subs = await _context.AppSubCodes
                .AsNoTracking()
                .OrderBy(s => s.AppSubCodeId)
                .ToListAsync();

            return Ok(subs);
        }
    }
}
