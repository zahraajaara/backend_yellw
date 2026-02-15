using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/Branches
    public class BranchesController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public BranchesController(YellowKalamContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Branch>>> GetBranches()
        {
            try
            {
                var branches = await _context.Branches
                    .AsNoTracking()
                    .OrderBy(b => b.BranchId)
                    .ToListAsync();

                return Ok(branches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "خطأ في الاتصال بقاعدة البيانات", error = ex.Message });
            }
        }
    }
}
