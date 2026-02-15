using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   // → /api/Ministry
    public class MinistryController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public MinistryController(YellowKalamContext context)
        {
            _context = context;
        }

        // ============================================================
        // GET: api/Ministry
        // ✅ UPDATED: Now supports search + pagination (skip/take)
        // Examples:
        //   GET api/Ministry                           → first 50
        //   GET api/Ministry?search=الصحة              → search by name
        //   GET api/Ministry?search=ministry&skip=0&take=20
        // ============================================================
        [HttpGet]
        public async Task<ActionResult<object>> GetMinistries(
            [FromQuery] string? search = null,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 50)
        {
            try
            {
                take = Math.Clamp(take, 1, 200);
                skip = Math.Max(skip, 0);

                var query = _context.Ministries.AsNoTracking().AsQueryable();

                // Apply search filter if provided
                if (!string.IsNullOrWhiteSpace(search))
                {
                    var searchTerm = search.Trim();
                    bool isNumeric = int.TryParse(searchTerm, out int searchNumber);

                    query = query.Where(m =>
                        (isNumeric && m.MinistryId == searchNumber) ||
                        (m.MinistryName != null && m.MinistryName.Contains(searchTerm)) ||
                        (m.MinistryAddress != null && m.MinistryAddress.Contains(searchTerm)) ||
                        (m.MinistryPhone != null && m.MinistryPhone.Contains(searchTerm)) ||
                        (m.MinistryWeb != null && m.MinistryWeb.Contains(searchTerm))
                    );
                }

                var totalCount = await query.CountAsync();

                var data = await query
                    .OrderBy(m => m.MinistryName)
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
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "خطأ في جلب البيانات", error = ex.Message });
            }
        }

        // GET: api/Ministry/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Ministry>> GetMinistry(int id)
        {
            try
            {
                var item = await _context.Ministries
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.MinistryId == id);

                if (item == null)
                    return NotFound();

                return item;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "خطأ في جلب البيانات", error = ex.Message });
            }
        }
    }
}