using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/AppCodeName
    public class AppCodeNameController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public AppCodeNameController(YellowKalamContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppCodeName>>> GetAppCodeNames()
        {
            var names = await _context.AppCodeNames
                .AsNoTracking()
                .OrderBy(n => n.AppCodeNameId)
                .ToListAsync();

            return Ok(names);
        }
    }
}
