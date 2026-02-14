using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    // ✅ FIX: Added dual routes so both /api/AppGroups AND /api/AppGroup work
    [ApiController]
    [Route("api/AppGroups")]
    [Route("api/AppGroup")]  // ← singular alias to fix 404
    public class AppGroupsController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public AppGroupsController(YellowKalamContext context)
        {
            _context = context;
        }

        // GET: api/AppGroups  OR  api/AppGroup
        // ✅ UPDATED: Now supports search + pagination
        [HttpGet]
        public async Task<ActionResult<object>> GetAppGroups(
            [FromQuery] string? search = null,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 50)
        {
            take = Math.Clamp(take, 1, 200);
            skip = Math.Max(skip, 0);

            var query = _context.AppGroups.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var searchTerm = search.Trim();
                bool isNumeric = int.TryParse(searchTerm, out int searchNumber);

                query = query.Where(g =>
                    (isNumeric && g.AppGroupId == searchNumber) ||
                    g.AppGroupName.Contains(searchTerm)
                );
            }

            var totalCount = await query.CountAsync();

            var data = await query
                .OrderBy(g => g.AppGroupId)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return Ok(new
            {
                total = totalCount,
                skip,
                take,
                data
            });
        }

        // GET: api/AppGroups/5  OR  api/AppGroup/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AppGroup>> GetAppGroup(int id)
        {
            var group = await _context.AppGroups
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.AppGroupId == id);

            if (group == null)
                return NotFound();

            return Ok(group);
        }
    }
}
