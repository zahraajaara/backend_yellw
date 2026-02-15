using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using YellowKalam.Api.Data;
using YellowKalam.Api.Dtos;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonsController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public PersonsController(YellowKalamContext context)
        {
            _context = context;
        }

        // ============================================================
        // GET: api/Persons
        // ✅ UPDATED: Now supports search + pagination (skip/take)
        // Examples:
        //   GET api/Persons                          → first 50
        //   GET api/Persons?search=أحمد              → search by name
        //   GET api/Persons?search=123&skip=0&take=20
        // ============================================================
        [HttpGet]
        public async Task<ActionResult<object>> GetPersons(
            [FromQuery] string? search = null,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 50)
        {
            try
            {
                take = Math.Clamp(take, 1, 200);
                skip = Math.Max(skip, 0);

                var query = _context.Persons.AsNoTracking().AsQueryable();

                // Apply search filter if provided
                if (!string.IsNullOrWhiteSpace(search))
                {
                    var searchTerm = search.Trim();
                    bool isNumeric = int.TryParse(searchTerm, out int searchNumber);

                    query = query.Where(p =>
                        (isNumeric && p.PersonId == searchNumber) ||
                        (p.PersonFname != null && p.PersonFname.Contains(searchTerm)) ||
                        (p.PersonMname != null && p.PersonMname.Contains(searchTerm)) ||
                        (p.PersonLname != null && p.PersonLname.Contains(searchTerm)) ||
                        (p.PersonMoName != null && p.PersonMoName.Contains(searchTerm)) ||
                        (p.PersonRegNum != null && p.PersonRegNum.Contains(searchTerm)) ||
                        (p.PersonCardNum != null && p.PersonCardNum.Contains(searchTerm)) ||
                        (p.TaxNum != null && p.TaxNum.Contains(searchTerm)) ||
                        (p.PersonRegPlace != null && p.PersonRegPlace.Contains(searchTerm)) ||
                        (p.Reference != null && p.Reference.Contains(searchTerm)) ||
                        (p.Notes != null && p.Notes.Contains(searchTerm))
                    );
                }

                var totalCount = await query.CountAsync();

                var data = await query
                    .OrderBy(p => p.PersonFname)
                    .ThenBy(p => p.PersonMname)
                    .ThenBy(p => p.PersonLname)
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
                return StatusCode(500, new { message = "خطأ في الاتصال بقاعدة البيانات", error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            try
            {
                var person = await _context.Persons.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.PersonId == id);

                if (person == null) return NotFound();
                return Ok(person);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "خطأ في الاتصال بقاعدة البيانات", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<Person>> CreatePerson([FromBody] PersonCreateDto dto)
        {
            if (dto == null) return BadRequest("Person is null");

            var sqlMin = new DateTime(1753, 1, 1);

            // must fit varchar(25)
            var userName = TrimTo(User?.Identity?.Name ?? "system", 25) ?? "system";

            // must fit char(1)
            var gender = TrimTo(dto.PersonGender, 1) ?? "M";

            var model = new Person
            {
                // DO NOT set PersonId

                AlSifaId = dto.AlSifaId, // (better: validate > 0)
                PersonFname = TrimTo(dto.PersonFname, 200) ?? "",

                PersonMname = TrimTo(dto.PersonMname, 25),
                PersonLname = TrimTo(dto.PersonLname, 25),
                PersonMoName = TrimTo(dto.PersonMoName, 25),

                Ref1 = TrimTo(dto.Ref1, 150),
                Ref2 = TrimTo(dto.Ref2, 150),
                Ref3 = TrimTo(dto.Ref3, 150),

                PersonGender = gender,

                // you don't want user to enter these:
                PersonDoB = sqlMin,
                PersonDateOfIssue = sqlMin,

                PersonNationId = dto.PersonNationId,
                PersonTypeId = dto.PersonTypeId,
                PersonMaritalStatusId = dto.PersonMaritalStatusId,

                TaxNum = TrimTo(dto.TaxNum, 50),
                Tvanum = TrimTo(dto.Tvanum, 50),
                BregNum = TrimTo(dto.BregNum, 50),

                CortAndY = TrimTo(dto.CortAndY, 50),
                Reference = TrimTo(dto.Reference, 100),
                Notes = TrimTo(dto.Notes, 50),

                CreatedBy = userName,
                UpdatedBy = userName,
                CreatedOn = DateTime.Now,
                LastUpdated = DateTime.Now,

                RowVersion = 1
            };

            try
            {
                _context.Persons.Add(model);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new
                {
                    message = "Database update failed",
                    details = ex.InnerException?.Message ?? ex.Message
                });
            }

            return CreatedAtAction(nameof(GetPerson), new { id = model.PersonId }, model);
        }

        private static string? TrimTo(string? v, int max)
        {
            if (string.IsNullOrWhiteSpace(v)) return null;
            v = v.Trim();
            return v.Length <= max ? v : v.Substring(0, max);
        }

        // ✅ UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] Person model)
        {
            if (model == null) return BadRequest("Person is null");
            if (id != model.PersonId) return BadRequest("ID mismatch");

            var exists = await _context.Persons.AnyAsync(p => p.PersonId == id);
            if (!exists) return NotFound();

            var userName = User?.Identity?.Name ?? "system";
            model.UpdatedBy = userName;
            model.LastUpdated = DateTime.Now;

            // same DOB / issue date protection
            var sqlMin = new DateTime(1753, 1, 1);

            if (model.PersonDoB < sqlMin) model.PersonDoB = sqlMin;
            if (model.PersonDateOfIssue < sqlMin) model.PersonDateOfIssue = sqlMin;

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new
                {
                    message = "Database update failed",
                    detail = ex.InnerException?.Message ?? ex.Message
                });
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null) return NotFound();

            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ─────────────────────────────────────────────
        // Helpers
        // ─────────────────────────────────────────────

        // Converts DateTime.MinValue to null (if nullable) OR sets safe value
        private static void FixSqlDateTimes(Person model)
        {
            // SQL Server minimum supported datetime
            var minSql = new DateTime(1753, 1, 1);

            // For each date-like property you might have, fix it:
            FixDate(model, "PersonDoB", minSql);
            FixDate(model, "PersonDateOfIssue", minSql);
            FixDate(model, "CreatedOn", minSql);
            FixDate(model, "LastUpdated", minSql);
            FixDate(model, "UpdatedOn", minSql);
        }

        private static void FixDate(object obj, string propName, DateTime minSql)
        {
            var prop = obj.GetType().GetProperty(propName);
            if (prop == null) return;

            var val = prop.GetValue(obj);
            if (val == null) return;

            if (val is DateTime dt)
            {
                if (dt == DateTime.MinValue || dt < minSql)
                {
                    // if nullable, set null, else set minSql
                    if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                        prop.SetValue(obj, null);
                    else
                        prop.SetValue(obj, minSql);
                }
            }
        }

        private static void TrySet(object obj, string propName, object value)
        {
            var prop = obj.GetType().GetProperty(propName);
            if (prop == null) return;
            if (!prop.CanWrite) return;

            // If it's nullable DateTime and value is DateTime, OK
            // If types mismatch, ignore silently to avoid runtime crash
            if (value == null)
            {
                prop.SetValue(obj, null);
                return;
            }

            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

            try
            {
                var converted = Convert.ChangeType(value, targetType);
                prop.SetValue(obj, converted);
            }
            catch
            {
                // ignore if can't convert
            }
        }
    }
}