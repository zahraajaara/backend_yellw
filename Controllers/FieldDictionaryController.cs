using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/FieldDictionary
    public class FieldDictionaryController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public FieldDictionaryController(YellowKalamContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FelidDictionary>>> GetFieldDictionary()
        {
            var fields = await _context.FelidDictionaries   // DbSet<FelidDictionary>
                .AsNoTracking()
                .OrderBy(f => f.FieldId)
                .ToListAsync();

            return Ok(fields);
        }
    }
}
