using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EhsaaController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public EhsaaController(YellowKalamContext context)
        {
            _context = context;
        }

        // GET: api/Ehsaa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ehsaa>>> GetAll()
        {
            return await _context.Ehsaas
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Ehsaa/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Ehsaa>> GetById(int id)
        {
            var item = await _context.Ehsaas.FindAsync(id);

            if (item == null)
                return NotFound();

            return item;
        }

        // POST: api/Ehsaa
        // (optional – in case you want to insert from UI or scripts)
        [HttpPost]
        public async Task<ActionResult<Ehsaa>> Create(Ehsaa ehsaa)
        {
            _context.Ehsaas.Add(ehsaa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ehsaa.EhsaaId }, ehsaa);
        }
    }
}
