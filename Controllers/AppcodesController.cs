using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // -> /api/Appcodes
    public class AppcodesController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public AppcodesController(YellowKalamContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appcode>>> GetAppcodes()
        {
            try
            {
                var codes = await _context.Appcodes
                    .AsNoTracking()
                    .OrderBy(c => c.AppcodeId)
                    .ToListAsync();

                return Ok(codes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "خطأ في الاتصال بقاعدة البيانات", error = ex.Message });
            }
        }

        // POST: api/Appcodes
        [HttpPost]
        public async Task<ActionResult<Appcode>> CreateAppcode([FromBody] System.Text.Json.JsonElement dto)
        {
            try
            {
                string? codeName = null;
                string? codeType = null;
                string? updatedBy = "web";

                // Extract values from JsonElement
                if (dto.TryGetProperty("codeName", out var codeNameProp))
                {
                    codeName = codeNameProp.GetString();
                }
                if (dto.TryGetProperty("codeType", out var codeTypeProp))
                {
                    codeType = codeTypeProp.GetString();
                }
                if (dto.TryGetProperty("updatedBy", out var updatedByProp))
                {
                    updatedBy = updatedByProp.GetString();
                }

                if (string.IsNullOrEmpty(codeName) || string.IsNullOrEmpty(codeType))
                {
                    return BadRequest(new { message = "codeName and codeType are required" });
                }

                // Generate AppcodeId - get max AppcodeId and increment by 1
                int newAppcodeId = 1;
                var maxAppcodeId = await _context.Appcodes.MaxAsync(c => (int?)c.AppcodeId);
                if (maxAppcodeId.HasValue)
                {
                    newAppcodeId = maxAppcodeId.Value + 1;
                }

                // Store codeType in AppcodeDescription for filtering
                var appcode = new Appcode
                {
                    AppcodeId = newAppcodeId,
                    AppCodeCode = codeType?.Substring(0, Math.Min(2, codeType.Length)).ToUpper() ?? "XX", // Use first 2 chars of codeType
                    AppcodeNameId = 1, // Default - this should be set based on codeType mapping
                    AppcodeName = codeName,
                    AppcodeAname = codeName, // Same as AppcodeName for Arabic
                    AppcodeDescription = codeType, // Store codeType here for filtering
                    IsDefault = "N",
                    HasChild = "N",
                    UpdatedBy = updatedBy ?? "web",
                    LastUpdated = DateTime.Now,
                    CreatedBy = updatedBy ?? "web",
                    CreatedOn = DateTime.Now,
                    RowVersion = 0,
                    TtimeStampa = new byte[8]
                };

                _context.Appcodes.Add(appcode);
                await _context.SaveChangesAsync();

                return Ok(appcode);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "حدث خطأ في الخادم", error = ex.Message });
            }
        }
    }
}
