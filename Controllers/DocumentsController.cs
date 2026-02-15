using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public DocumentsController(YellowKalamContext context)
        {
            _context = context;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            return await _context.Documents.AsNoTracking().ToListAsync();
        }

        // GET: api/Documents/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var doc = await _context.Documents.FindAsync(id);

            if (doc == null)
                return NotFound();

            return doc;
        }
    }
}
