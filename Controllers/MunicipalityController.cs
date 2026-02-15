using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   // → /api/Municipality
    public class MunicipalityController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public MunicipalityController(YellowKalamContext context)
        {
            _context = context;
        }

        // ============================================================
        // GET: api/Municipality
        // ✅ UPDATED: Now supports search + pagination (skip/take)
        // Examples:
        //   GET api/Municipality                        → first 50
        //   GET api/Municipality?search=بيروت           → search by name
        //   GET api/Municipality?search=city&skip=0&take=20
        // ============================================================
        [HttpGet]
        public async Task<ActionResult<object>> GetMunicipalities(
            [FromQuery] string? search = null,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 50)
        {
            try
            {
                take = Math.Clamp(take, 1, 200);
                skip = Math.Max(skip, 0);

                var query = _context.Municipalities.AsNoTracking().AsQueryable();

                // Apply search filter if provided
                if (!string.IsNullOrWhiteSpace(search))
                {
                    var searchTerm = search.Trim();
                    bool isNumeric = int.TryParse(searchTerm, out int searchNumber);

                    query = query.Where(m =>
                        (isNumeric && m.MunicipalityId == searchNumber) ||
                        (m.MunicipalityName != null && m.MunicipalityName.Contains(searchTerm)) ||
                        (m.MunicipalityCityName != null && m.MunicipalityCityName.Contains(searchTerm)) ||
                        (m.MunicipalityMainAddress != null && m.MunicipalityMainAddress.Contains(searchTerm)) ||
                        (m.MunicipalityTel != null && m.MunicipalityTel.Contains(searchTerm)) ||
                        (m.MunicipalityWeb != null && m.MunicipalityWeb.Contains(searchTerm)) ||
                        (m.Ref1 != null && m.Ref1.Contains(searchTerm)) ||
                        (m.Ref2 != null && m.Ref2.Contains(searchTerm))
                    );
                }

                var totalCount = await query.CountAsync();

                var data = await query
                    .OrderBy(m => m.MunicipalityName)
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

        // GET: api/Municipality/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Municipality>> GetMunicipality(int id)
        {
            try
            {
                var item = await _context.Municipalities
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.MunicipalityId == id);

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