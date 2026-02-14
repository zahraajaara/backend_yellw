using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using YellowKalam.Api.Data;
using YellowKalam.Api.Dtos;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IhalerController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public IhalerController(YellowKalamContext context)
        {
            _context = context;
        }

        public class AssignIhalerDto
        {
            [Required]
            public List<int> IhalerIds { get; set; } = new();

            [Required]
            public int AppUserId { get; set; }
        }

        public class SaveRoutingRequest
        {
            [Required]
            public List<int> IhalerIds { get; set; } = new();
        }

        // ============================================================
        // GET: api/Ihaler
        // UPDATED: Now supports search param for referrals page
        //   GET api/Ihaler?search=3968&take=30&skip=0
        // Searches: IhalerId, DemandId, IhalerDesc, IhalerStatus,
        //           KarrarNum, and related Demand number/description
        // ============================================================
        [HttpGet]
        public async Task<ActionResult<object>> GetIhaler(
            [FromQuery] string? search = null,
            [FromQuery] int take = 30,
            [FromQuery] int skip = 0)
        {
            try
            {
                take = Math.Clamp(take, 1, 200);
                skip = Math.Max(skip, 0);

                var query = _context.Ihalers
                    .AsNoTracking()
                    .Include(i => i.Demand)
                    .AsQueryable();

                if (!string.IsNullOrWhiteSpace(search))
                {
                    var searchTerm = search.Trim();
                    bool isNumeric = int.TryParse(searchTerm, out int searchNumber);

                    query = query.Where(i =>
                        (isNumeric && (i.IhalerId == searchNumber ||
                                       i.DemandId == searchNumber ||
                                       (i.Demand != null && i.Demand.RegN == searchNumber))) ||
                        (i.IhalerDesc != null && i.IhalerDesc.Contains(searchTerm)) ||
                        (i.IhalerStatus != null && i.IhalerStatus.Contains(searchTerm)) ||
                        (i.KarrarNum != null && i.KarrarNum.Contains(searchTerm)) ||
                        (i.Demand != null && i.Demand.Title != null && i.Demand.Title.Contains(searchTerm)) ||
                        (i.Demand != null && i.Demand.CallerDesc != null && i.Demand.CallerDesc.Contains(searchTerm)) ||
                        (i.Demand != null && i.Demand.FinalStatus != null && i.Demand.FinalStatus.Contains(searchTerm))
                    );
                }

                var totalCount = await query.CountAsync();

                var data = await query
                    .OrderByDescending(i => i.IhalerId)
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
                return StatusCode(500, new { message = "Error fetching data", error = ex.Message });
            }
        }


        // GET: api/Ihaler/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Ihaler>> GetIhalerById(int id)
        {
            try
            {
                var item = await _context.Ihalers.FindAsync(id);

                if (item == null)
                    return NotFound();

                return item;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error", error = ex.Message });
            }
        }

        // POST: api/Ihaler/assign
        [HttpPost("assign")]
        public async Task<IActionResult> AssignToUser([FromBody] AssignIhalerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.IhalerIds == null || dto.IhalerIds.Count == 0)
                return BadRequest("No IhalerIds specified.");

            var now = DateTime.Now;

            var ihalersToUpdate = new List<Ihaler>();

            foreach (var id in dto.IhalerIds)
            {
                var ih = await _context.Ihalers.FindAsync(id);
                if (ih != null)
                {
                    ih.IhalerStatus = "Assigned";
                    ih.UpdatedBy = "WebApp";
                    ih.LastUpdated = now;
                    ihalersToUpdate.Add(ih);
                }
            }

            if (ihalersToUpdate.Count == 0)
                return NotFound("No Ihaler rows found for the given ids.");

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while saving Ihaler updates",
                    ex.Message,
                    Inner = ex.InnerException?.Message
                });
            }

            return Ok(new
            {
                message = "Ihaler rows assigned successfully.",
                dto.AppUserId,
                updatedCount = ihalersToUpdate.Count,
                ids = ihalersToUpdate.Select(x => x.IhalerId).ToList()
            });
        }

        public class DeleteIhalerRequest
        {
            [Required]
            public List<int> IhalerIds { get; set; } = new();
        }

        // POST api/Ihaler/delete
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteIhalerBulk([FromBody] DeleteIhalerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.IhalerIds == null || request.IhalerIds.Count == 0)
                return BadRequest("No IhalerIds specified.");

            var ids = request.IhalerIds.Distinct().ToList();
            var totalDeleted = 0;

            foreach (var id in ids)
            {
                var rows = await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"DELETE FROM dbo.Ihaler WHERE IhalerId = {id}");
                totalDeleted += rows;
            }

            return Ok();
        }

        // ============================================================
        // POST api/Ihaler/save-temporary
        // ? FIXED: Better error handling and removed IhalerSign CAST issue
        // ============================================================
        [HttpPost("save-temporary")]
        public async Task<IActionResult> SaveTemporary([FromBody] SaveRoutingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.IhalerIds == null || request.IhalerIds.Count == 0)
                return BadRequest("No IhalerIds specified.");

            try
            {
                var totalInserted = 0;

                foreach (var ihId in request.IhalerIds.Distinct())
                {
                    var rows = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                INSERT INTO dbo.TemporaryRouting
                (
                    DemandId,
                    IhalerNum,
                    AppGroupID,
                    IhalerDate,
                    IhalerTypeId,
                    IhalerStatus,
                    IhalerRevissionDate,
                    KarrarNum,
                    KarrarDate,
                    IhalerDesc,
                    ReceivedDate,
                    IsLast,
                    UpdatedBy,
                    LastUpdated,
                    TTimeStamp
                )
                SELECT
                    DemandId,
                    IhalerNum,
                    AppGroupID,
                    IhalerDate,
                    IhalerTypeId,
                    IhalerStatus,
                    IhalerRevissionDate,
                    KarrarNum,
                    KarrarDate,
                    IhalerDesc,
                    ReceivedDate,
                    CASE 
                        WHEN IsLast IN ('Y', 'y', '1') THEN 1
                        WHEN IsLast IN ('N', 'n', '0') THEN 0
                        ELSE NULL
                    END,
                    UpdatedBy,
                    LastUpdated,
                    GETDATE()
                FROM dbo.Ihaler
                WHERE IhalerId = {ihId};
            ");

                    totalInserted += rows;
                }

                if (totalInserted == 0)
                    return NotFound("No Ihaler rows found for the given ids.");

                return Ok(new
                {
                    message = "Temporary routing saved successfully.",
                    insertedCount = totalInserted,
                    ids = request.IhalerIds
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error saving temporary routing",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }
        // ============================================================
        // POST api/Ihaler/save-final
        // ? FIXED: Better error handling and removed IhalerSign CAST issue
        // ============================================================
        [HttpPost("save-final")]
        public async Task<IActionResult> SaveFinal([FromBody] SaveRoutingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.IhalerIds == null || request.IhalerIds.Count == 0)
                return BadRequest("No IhalerIds specified.");

            try
            {
                var totalInserted = 0;

                foreach (var ihId in request.IhalerIds.Distinct())
                {
                    var rows = await _context.Database.ExecuteSqlInterpolatedAsync($@"
                INSERT INTO dbo.TransactionRouting
                (
                    DemandId,
                    IhalerNum,
                    AppGroupID,
                    IhalerDate,
                    IhalerTypeId,
                    IhalerStatus,
                    IhalerRevissionDate,
                    KarrarNum,
                    KarrarDate,
                    IhalerDesc,
                    ReceivedDate,
                    IsLast,
                    UpdatedBy,
                    LastUpdated,
                    TTimeStamp
                )
                SELECT
                    DemandId,
                    IhalerNum,
                    AppGroupID,
                    IhalerDate,
                    IhalerTypeId,
                    IhalerStatus,
                    IhalerRevissionDate,
                    KarrarNum,
                    KarrarDate,
                    IhalerDesc,
                    ReceivedDate,
                    CASE 
                        WHEN IsLast IN ('Y', 'y', '1') THEN 1
                        WHEN IsLast IN ('N', 'n', '0') THEN 0
                        ELSE NULL
                    END,
                    UpdatedBy,
                    LastUpdated,
                    GETDATE()
                FROM dbo.Ihaler
                WHERE IhalerId = {ihId};
            ");

                    totalInserted += rows;
                }

                if (totalInserted == 0)
                    return NotFound("No Ihaler rows found for the given ids.");

                return Ok(new
                {
                    message = "Final routing saved successfully.",
                    insertedCount = totalInserted,
                    ids = request.IhalerIds
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error saving final routing",
                    error = ex.Message,
                    innerError = ex.InnerException?.Message
                });
            }
        }

        // GET: api/Ihaler/pending
        [HttpGet("pending")]
        public async Task<ActionResult<IEnumerable<object>>> GetPendingIhalers([FromQuery] int? userId = null)
        {
            try
            {
                var query = _context.Ihalers
                    .Include(i => i.Demand)
                    .Where(i => i.IhalerStatus == null || i.IhalerStatus == "Pending" || i.IhalerStatus == "Assigned");

                if (userId.HasValue)
                {
                    query = query.Where(i => i.AppGroupId == userId.Value);
                }

                var result = await query
                    .OrderByDescending(i => i.IhalerDate)
                    .Take(50)
                    .Select(i => new
                    {
                        i.IhalerId,
                        i.DemandId,
                        i.IhalerDate,
                        i.IhalerDesc,
                        i.IhalerStatus,
                        i.AppGroupId,
                        Demand = i.Demand != null ? new
                        {
                            i.Demand.DemandId,
                            i.Demand.DemandCode,
                            i.Demand.RegN,
                            i.Demand.Title,
                            i.Demand.TrxnSubject,
                            i.Demand.CallerName,
                            i.Demand.TrxnDate,
                            i.Demand.FinalStatus
                        } : null
                    })
                    .ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error", error = ex.Message });
            }
        }
    }
}
