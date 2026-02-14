using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    public class DemandDetailsDto
    {
        public Demand Demand { get; set; } = null!;
        public List<DemandAvailableDoc> AvailableDocs { get; set; } = new();
        public List<DemandsField> Fields { get; set; } = new();
        public DemandsName? DemandName { get; set; }
        public List<DemandsNameDocument> NameDocuments { get; set; } = new();
        public DemandStatus? Status { get; set; }
    }

    public class CreateDemandDto
    {
        public int? RegN { get; set; }
        public int? YearDate { get; set; }
        public string? DemandCode { get; set; }
        public string? Title { get; set; }
        public string? TrxnSubject { get; set; }
        public string? FinalStatus { get; set; }
        public string? PdfSourcePath { get; set; }

        public int? RowId { get; set; }
        public int? ArticleCategoryId { get; set; }

        public int? CallerName { get; set; }
        public string? CallerDesc { get; set; }
        public string? CallerJob { get; set; }

        public int? Applicant { get; set; }
        public string? ApplicantDesc { get; set; }
        public string? ApplicantJob { get; set; }

        public string? TrxnDate { get; set; }
        public string? ValueDate { get; set; }
        public string? CompletionDate { get; set; }
        public string? ReceivedDate { get; set; }

        public string? ResultExp { get; set; }
        public string? DetectionNum { get; set; }

        public string? RealEstate { get; set; }
        public string? Block { get; set; }
        public string? Part { get; set; }
        public string? Floor { get; set; }
        public string? Hay { get; set; }
        public string? Near { get; set; }
        public string? Realestatearea { get; set; }

        public int? OwnerName { get; set; }
        public int? Renter { get; set; }
        public string? MsalePrice { get; set; }
        public string? Amount { get; set; }

        public string? Signature { get; set; }
        public string? Note2 { get; set; }
        public string? Note3 { get; set; }

        public string? Ref1 { get; set; }
        public string? Ref2 { get; set; }
        public string? Ref3 { get; set; }
        public string? Ref4 { get; set; }
        public string? Ref5 { get; set; }
        public string? Ref6 { get; set; }
        public string? Ref7 { get; set; }
        public string? Ref8 { get; set; }
        public string? Ref9 { get; set; }

        public string? CallerIs { get; set; }
        public string? Stype { get; set; }

        // New field
        public List<string>? DocumentsNeeded { get; set; }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class DemandsController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public DemandsController(YellowKalamContext context)
        {
            _context = context;
        }

        // ============================================================
        // GET: api/Demands
        // ✅ UPDATED: Now supports search + pagination (skip/take)
        // ✅ FIXED: Returns actual caller person name instead of ID
        // Examples:
        //   GET api/Demands                        → first 50
        //   GET api/Demands?search=5717             → search by number
        //   GET api/Demands?search=شكوى&skip=0&take=20
        // ============================================================
        [HttpGet]
        public async Task<ActionResult<object>> GetDemands(
            [FromQuery] string? search = null,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 50)
        {
            try
            {
                take = Math.Clamp(take, 1, 200);
                skip = Math.Max(skip, 0);

                var query = _context.Demands
                    .Include(d => d.CallerNameNavigation) // Include Person navigation
                    .AsNoTracking()
                    .AsQueryable();

                // Apply search filter if provided
                if (!string.IsNullOrWhiteSpace(search))
                {
                    var searchTerm = search.Trim();
                    bool isNumeric = int.TryParse(searchTerm, out int searchNumber);

                    query = query.Where(d =>
                        (isNumeric && (d.DemandId == searchNumber || d.RegN == searchNumber)) ||
                        (d.DemandCode != null && d.DemandCode.Contains(searchTerm)) ||
                        (d.Title != null && d.Title.Contains(searchTerm)) ||
                        (d.TrxnSubject != null && d.TrxnSubject.Contains(searchTerm)) ||
                        (d.CallerDesc != null && d.CallerDesc.Contains(searchTerm)) ||
                        (d.FinalStatus != null && d.FinalStatus.Contains(searchTerm)) ||
                        (d.ResultExp != null && d.ResultExp.Contains(searchTerm)) ||
                        (d.Note2 != null && d.Note2.Contains(searchTerm))
                    );
                }

                var totalCount = await query.CountAsync();

                var data = await query
                    .OrderByDescending(d => d.DemandId)
                    .Skip(skip)
                    .Take(take)
                    .Select(d => new
                    {
                        d.DemandId,
                        d.RegN,
                        d.DemandCode,
                        d.YearDate,
                        d.RowId,
                        d.Title,
                        d.TrxnSubject,
                        d.FinalStatus,
                        d.TrxnDate,
                        d.ValueDate,
                        d.CompletionDate,
                        d.ReceivedDate,
                        CallerNameId = d.CallerName,
                        CallerName = d.CallerNameNavigation != null
                            ? $"{d.CallerNameNavigation.PersonFname} {d.CallerNameNavigation.PersonMname} {d.CallerNameNavigation.PersonLname}".Trim()
                            : d.CallerDesc,
                        d.CallerDesc,
                        d.CallerJob,
                        d.Applicant,
                        d.ApplicantDesc,
                        d.ApplicantJob,
                        d.ResultExp,
                        d.DetectionNum,
                        d.RealEstate,
                        d.Block,
                        d.Part,
                        d.Floor,
                        d.Hay,
                        d.Near,
                        d.Realestatearea,
                        d.PdfSourcePath,
                        d.DocumentsNeeded,
                        d.Note2,
                        d.Note3,
                        regNumber = d.DemandCode + d.YearDate.ToString() + "/" + (d.RegN.HasValue ? d.RegN.Value.ToString() : "0")
                    })
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
        // ============================================================
        // GET: api/Demands/search
        // ✅ UPDATED: Added pdfSourcePath search parameter
        // ✅ FIXED: Exact match filtering for demandCode (ش vs م)
        // ✅ FIXED: Returns actual caller person name instead of ID
        // Supports: demandCode, demandTypeId, finalStatus, fromDate,
        //           toDate, department, pdfSourcePath, search (text), skip, take
        // ============================================================
        [HttpGet("search")]
        public async Task<ActionResult<object>> SearchDemands(
            [FromQuery] string? demandCode = null,
            [FromQuery] int? demandTypeId = null,
            [FromQuery] string? finalStatus = null,
            [FromQuery] DateTime? fromDate = null,
            [FromQuery] DateTime? toDate = null,
            [FromQuery] string? department = null,
            [FromQuery] string? pdfSourcePath = null,
            [FromQuery] string? search = null,
            [FromQuery] int skip = 0,
            [FromQuery] int take = 50)
        {
            try
            {
                take = Math.Clamp(take, 1, 500);
                skip = Math.Max(skip, 0);

                var query = _context.Demands
                    .Include(d => d.CallerNameNavigation) // Include Person navigation
                    .AsNoTracking()
                    .AsQueryable();

                // ✅ FIXED: Filter by demand code with EXACT match (not Contains)
                if (!string.IsNullOrWhiteSpace(demandCode))
                {
                    var exactCode = demandCode.Trim();
                    query = query.Where(d => d.DemandCode == exactCode);
                }

                // Filter by demand type ID (RowId from DemandsName)
                if (demandTypeId.HasValue)
                {
                    query = query.Where(d => d.RowId == demandTypeId.Value);
                }

                // Filter by final status
                if (!string.IsNullOrWhiteSpace(finalStatus))
                {
                    query = query.Where(d => d.FinalStatus == finalStatus.Trim());
                }

                // Filter by date range (using TrxnDate)
                if (fromDate.HasValue)
                {
                    query = query.Where(d => d.TrxnDate >= fromDate.Value);
                }
                if (toDate.HasValue)
                {
                    var endDate = toDate.Value.Date.AddDays(1);
                    query = query.Where(d => d.TrxnDate < endDate);
                }

                // Filter by department
                if (!string.IsNullOrWhiteSpace(department))
                {
                    query = query.Where(d =>
                        (d.Ref1 != null && d.Ref1.Contains(department.Trim())) ||
                        (d.Hay != null && d.Hay.Contains(department.Trim()))
                    );
                }

                // Filter by PDF source path
                if (!string.IsNullOrWhiteSpace(pdfSourcePath))
                {
                    query = query.Where(d => d.PdfSourcePath != null && d.PdfSourcePath.Contains(pdfSourcePath.Trim()));
                }

                // General text search
                if (!string.IsNullOrWhiteSpace(search))
                {
                    var searchTerm = search.Trim();
                    bool isNumeric = int.TryParse(searchTerm, out int searchNumber);

                    query = query.Where(d =>
                        (isNumeric && (d.DemandId == searchNumber || d.RegN == searchNumber)) ||
                        (d.Title != null && d.Title.Contains(searchTerm)) ||
                        (d.TrxnSubject != null && d.TrxnSubject.Contains(searchTerm)) ||
                        (d.CallerDesc != null && d.CallerDesc.Contains(searchTerm)) ||
                        (d.ResultExp != null && d.ResultExp.Contains(searchTerm)) ||
                        (d.PdfSourcePath != null && d.PdfSourcePath.Contains(searchTerm))
                    );
                }

                var totalCount = await query.CountAsync();

                var data = await query
                    .OrderByDescending(d => d.DemandId)
                    .Skip(skip)
                    .Take(take)
                    .Select(d => new
                    {
                        d.DemandId,
                        d.RegN,
                        d.DemandCode,
                        d.YearDate,
                        d.RowId,
                        d.Title,
                        d.TrxnSubject,
                        d.FinalStatus,
                        d.TrxnDate,
                        d.ValueDate,
                        d.CompletionDate,
                        d.ReceivedDate,
                        CallerNameId = d.CallerName,
                        CallerName = d.CallerNameNavigation != null
                            ? $"{d.CallerNameNavigation.PersonFname} {d.CallerNameNavigation.PersonMname} {d.CallerNameNavigation.PersonLname}".Trim()
                            : d.CallerDesc,
                        d.CallerDesc,
                        d.CallerJob,
                        d.Applicant,
                        d.ApplicantDesc,
                        d.ApplicantJob,
                        d.ResultExp,
                        d.DetectionNum,
                        d.RealEstate,
                        d.Block,
                        d.Part,
                        d.Floor,
                        d.Hay,
                        d.Near,
                        d.Realestatearea,
                        d.PdfSourcePath,
                        d.DocumentsNeeded,
                        regNumber = d.DemandCode + d.YearDate.ToString() + "/" + (d.RegN.HasValue ? d.RegN.Value.ToString() : "0")
                    })
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
                return StatusCode(500, new { message = "خطأ في البحث", error = ex.Message });
            }
        }

        // GET: api/Demands/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Demand>> GetDemand(int id)
        {
            try
            {
                var demand = await _context.Demands
                    .AsNoTracking()
                    .FirstOrDefaultAsync(d => d.DemandId == id);

                if (demand == null)
                    return NotFound();

                return demand;
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "خطأ في الاتصال بقاعدة البيانات", error = ex.Message });
            }
        }

        // POST: api/Demands
        // POST: api/Demands
        [HttpPost]
        public async Task<ActionResult<Demand>> CreateDemand([FromBody] CreateDemandDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var now = DateTime.Now;

            var year = (!dto.YearDate.HasValue || dto.YearDate.Value <= 0)
                ? now.Year
                : dto.YearDate.Value;

            var regN = (!dto.RegN.HasValue || dto.RegN.Value <= 0)
                ? 0
                : dto.RegN.Value;

            var maxDemandId = await _context.Demands
                .MaxAsync(d => (int?)d.DemandId) ?? 0;

            var newDemandId = maxDemandId + 1;

            DateTime trxnDate = !string.IsNullOrWhiteSpace(dto.TrxnDate)
                ? DateTime.Parse(dto.TrxnDate)
                : now;

            DateTime valueDate = !string.IsNullOrWhiteSpace(dto.ValueDate)
                ? DateTime.Parse(dto.ValueDate)
                : now;

            DateTime completionDate = !string.IsNullOrWhiteSpace(dto.CompletionDate)
                ? DateTime.Parse(dto.CompletionDate)
                : new DateTime(1900, 1, 1);

            DateTime receivedDate = !string.IsNullOrWhiteSpace(dto.ReceivedDate)
                ? DateTime.Parse(dto.ReceivedDate)
                : new DateTime(1900, 1, 1);

            var demand = new Demand
            {
                DemandId = newDemandId,
                RegN = regN,
                YearDate = year,
                DemandCode = dto.DemandCode ?? string.Empty,
                Title = dto.Title ?? string.Empty,
                TrxnSubject = dto.TrxnSubject ?? string.Empty,
                FinalStatus = dto.FinalStatus ?? "قيد الدرس",
                PdfSourcePath = dto.PdfSourcePath ?? string.Empty,
                TrxnDate = trxnDate,
                ValueDate = valueDate,
                CompletionDate = completionDate,
                RowId = dto.RowId ?? 1,
                ArticleCategoryId = dto.ArticleCategoryId ?? 0,
                CallerName = dto.CallerName ?? 1,
                CallerDesc = dto.CallerDesc ?? string.Empty,
                CallerJob = dto.CallerJob ?? string.Empty,
                Applicant = dto.Applicant ?? 1,
                ApplicantDesc = dto.ApplicantDesc ?? string.Empty,
                ApplicantJob = dto.ApplicantJob ?? string.Empty,
                ResultExp = dto.ResultExp ?? string.Empty,
                DetectionNum = dto.DetectionNum ?? string.Empty,
                RealEstate = dto.RealEstate ?? string.Empty,
                Part = dto.Part ?? string.Empty,
                Floor = dto.Floor ?? string.Empty,
                Block = dto.Block ?? string.Empty,
                Hay = dto.Hay ?? string.Empty,
                Near = dto.Near ?? string.Empty,
                Realestatearea = dto.Realestatearea ?? string.Empty,
                OwnerName = dto.OwnerName ?? 1,
                Renter = dto.Renter ?? 1,
                MsalePrice = dto.MsalePrice ?? string.Empty,
                Amount = dto.Amount ?? string.Empty,
                Signature = dto.Signature ?? string.Empty,
                Note2 = dto.Note2 ?? string.Empty,
                Note3 = dto.Note3 ?? string.Empty,
                Ref1 = dto.Ref1 ?? string.Empty,
                Ref2 = dto.Ref2 ?? string.Empty,
                Ref3 = dto.Ref3 ?? string.Empty,
                Ref4 = dto.Ref4 ?? string.Empty,
                Ref5 = dto.Ref5 ?? string.Empty,
                Ref6 = dto.Ref6 ?? string.Empty,
                Ref7 = dto.Ref7 ?? string.Empty,
                Ref8 = dto.Ref8 ?? string.Empty,
                Ref9 = dto.Ref9 ?? string.Empty,
                CallerIs = dto.CallerIs ?? string.Empty,
                Stype = dto.Stype ?? string.Empty,
                ReceivedDate = receivedDate,
                DocumentsNeededList = dto.DocumentsNeeded, // New field
                RowVersion = 1,
                UpdatedBy = "WebApp",
                LastUpdated = now,
                CreatedBy = "WebApp",
                CreatedOn = now,
                TtimeStamp = null
            };

            try
            {
                _context.Demands.Add(demand);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = "Database error while creating demand",
                    error = ex.Message,
                    inner = ex.InnerException?.Message
                });
            }

            return CreatedAtAction(
                nameof(GetDemandDetails),
                new { id = demand.DemandId },
                demand
            );
        }

        // GET: api/Demands/{id}/details
        [HttpGet("{id:int}/details")]
        public async Task<ActionResult<DemandDetailsDto>> GetDemandDetails(int id)
        {
            var demand = await _context.Demands
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DemandId == id);

            if (demand == null)
                return NotFound();

            var availableDocs = await _context.DemandAvailableDocs
                .AsNoTracking()
                .Where(x => x.DemandId == id)
                .ToListAsync();

            var allFields = await _context.DemandsFields
                .AsNoTracking()
                .ToListAsync();
            var fields = allFields.Where(x => x.DemandId == id).ToList();

            var allNames = await _context.DemandsNames
                .AsNoTracking()
                .ToListAsync();
            var demandName = allNames.FirstOrDefault(x => x.DemandId == id);

            var allNameDocs = await _context.DemandsNameDocuments
                .AsNoTracking()
                .ToListAsync();
            var nameDocuments = allNameDocs.Where(x => x.DemandId == id).ToList();

            var allStatuses = await _context.DemandStatuses
                .AsNoTracking()
                .ToListAsync();
            var status = allStatuses.FirstOrDefault(x => x.DemandId == id);

            var dto = new DemandDetailsDto
            {
                Demand = demand,
                AvailableDocs = availableDocs,
                Fields = fields,
                DemandName = demandName,
                NameDocuments = nameDocuments,
                Status = status
            };

            return dto;
        }

        public class UpdateDemandDto
        {
            public int DemandId { get; set; }
            public int? RegN { get; set; }
            public int? YearDate { get; set; }
            public string? DemandCode { get; set; }
            public string? Title { get; set; }
            public string? TrxnSubject { get; set; }
            public string? FinalStatus { get; set; }
            public string? PdfSourcePath { get; set; }
            public string? ResultExp { get; set; }
            public string? DetectionNum { get; set; }
            public string? RealEstate { get; set; }
            public string? Block { get; set; }
            public string? Part { get; set; }
            public string? Floor { get; set; }
            public string? Hay { get; set; }
            public string? Near { get; set; }
            public string? RealEstateArea { get; set; }
            public string? Signature { get; set; }
            public string? Note2 { get; set; }
            public string? Note3 { get; set; }
            public string? Ref1 { get; set; }
            public string? Ref2 { get; set; }
            public string? Ref3 { get; set; }
            public string? Ref4 { get; set; }
            public string? Ref5 { get; set; }
            public string? Ref6 { get; set; }
            public string? Ref7 { get; set; }
            public string? Ref8 { get; set; }
            public string? Ref9 { get; set; }
            public int? CallerName { get; set; }
            public string? CallerDesc { get; set; }
            public string? CallerJob { get; set; }
            public int? Applicant { get; set; }
            public string? ApplicantDesc { get; set; }
            public string? ApplicantJob { get; set; }
            public string? CallerIs { get; set; }
            public string? Stype { get; set; }
            public string? MsalePrice { get; set; }
            public string? Amount { get; set; }
            public string? ReceivedDate { get; set; }
            public string? TrxnDate { get; set; }
            public string? ValueDate { get; set; }
            public string? CompletionDate { get; set; }

            // New field
            public List<string>? DocumentsNeeded { get; set; }
        }

        // PUT: api/Demands/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Demand>> UpdateDemand(int id, [FromBody] UpdateDemandDto dto)
        {
            if (id != dto.DemandId)
                return BadRequest("DemandId mismatch");

            var demand = await _context.Demands.FirstOrDefaultAsync(d => d.DemandId == id);
            if (demand == null)
                return NotFound();

            demand.RegN = dto.RegN ?? demand.RegN;
            demand.YearDate = dto.YearDate ?? demand.YearDate;
            demand.DemandCode = dto.DemandCode ?? demand.DemandCode;
            demand.Title = dto.Title ?? demand.Title;
            demand.TrxnSubject = dto.TrxnSubject ?? demand.TrxnSubject;
            demand.FinalStatus = dto.FinalStatus ?? demand.FinalStatus;
            demand.PdfSourcePath = dto.PdfSourcePath ?? demand.PdfSourcePath;
            demand.ResultExp = dto.ResultExp ?? demand.ResultExp;
            demand.DetectionNum = dto.DetectionNum ?? demand.DetectionNum;
            demand.RealEstate = dto.RealEstate ?? demand.RealEstate;
            demand.Block = dto.Block ?? demand.Block;
            demand.Part = dto.Part ?? demand.Part;
            demand.Floor = dto.Floor ?? demand.Floor;
            demand.Hay = dto.Hay ?? demand.Hay;
            demand.Near = dto.Near ?? demand.Near;
            demand.Realestatearea = dto.RealEstateArea ?? demand.Realestatearea;
            demand.Signature = dto.Signature ?? demand.Signature;
            demand.Note2 = dto.Note2 ?? demand.Note2;
            demand.Note3 = dto.Note3 ?? demand.Note3;
            demand.Ref1 = dto.Ref1 ?? demand.Ref1;
            demand.Ref2 = dto.Ref2 ?? demand.Ref2;
            demand.Ref3 = dto.Ref3 ?? demand.Ref3;
            demand.Ref4 = dto.Ref4 ?? demand.Ref4;
            demand.Ref5 = dto.Ref5 ?? demand.Ref5;
            demand.Ref6 = dto.Ref6 ?? demand.Ref6;
            demand.Ref7 = dto.Ref7 ?? demand.Ref7;
            demand.Ref8 = dto.Ref8 ?? demand.Ref8;
            demand.Ref9 = dto.Ref9 ?? demand.Ref9;
            demand.CallerName = dto.CallerName ?? demand.CallerName;
            demand.CallerDesc = dto.CallerDesc ?? demand.CallerDesc;
            demand.CallerJob = dto.CallerJob ?? demand.CallerJob;
            demand.Applicant = dto.Applicant ?? demand.Applicant;
            demand.ApplicantDesc = dto.ApplicantDesc ?? demand.ApplicantDesc;
            demand.ApplicantJob = dto.ApplicantJob ?? demand.ApplicantJob;
            demand.CallerIs = dto.CallerIs ?? demand.CallerIs;
            demand.Stype = dto.Stype ?? demand.Stype;
            demand.MsalePrice = dto.MsalePrice ?? demand.MsalePrice;
            demand.Amount = dto.Amount ?? demand.Amount;

            // Update documentsNeeded if provided
            if (dto.DocumentsNeeded != null)
                demand.DocumentsNeededList = dto.DocumentsNeeded;

            if (!string.IsNullOrWhiteSpace(dto.TrxnDate))
                demand.TrxnDate = DateTime.Parse(dto.TrxnDate);
            if (!string.IsNullOrWhiteSpace(dto.ValueDate))
                demand.ValueDate = DateTime.Parse(dto.ValueDate);
            if (!string.IsNullOrWhiteSpace(dto.CompletionDate))
                demand.CompletionDate = DateTime.Parse(dto.CompletionDate);
            if (!string.IsNullOrWhiteSpace(dto.ReceivedDate))
                demand.ReceivedDate = DateTime.Parse(dto.ReceivedDate);

            demand.LastUpdated = DateTime.Now;
            demand.UpdatedBy = "WebApp";

            await _context.SaveChangesAsync();
            return Ok(demand);
        }

        // DELETE: api/Demands/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteDemand(int id)
        {
            var demand = await _context.Demands
                .FirstOrDefaultAsync(d => d.DemandId == id);

            if (demand == null)
                return NotFound();

            var allIhaler = await _context.Ihalers.ToListAsync();
            var hasIhaler = allIhaler.Any(x => x.DemandId == id);

            if (hasIhaler)
            {
                return Conflict(new
                {
                    code = "DEMAND_HAS_IHALER",
                    message = "Cannot delete this demand because there are Ihaler records linked to it."
                });
            }

            var allAvailableDocs = await _context.DemandAvailableDocs.ToListAsync();
            var availableDocs = allAvailableDocs.Where(x => x.DemandId == id).ToList();
            var allFields = await _context.DemandsFields.ToListAsync();
            var fields = allFields.Where(x => x.DemandId == id).ToList();
            var allNames = await _context.DemandsNames.ToListAsync();
            var names = allNames.Where(x => x.DemandId == id).ToList();
            var allNameDocs = await _context.DemandsNameDocuments.ToListAsync();
            var nameDocuments = allNameDocs.Where(x => x.DemandId == id).ToList();
            var allStatuses = await _context.DemandStatuses.ToListAsync();
            var statuses = allStatuses.Where(x => x.DemandId == id).ToList();

            _context.DemandAvailableDocs.RemoveRange(availableDocs);
            _context.DemandsFields.RemoveRange(fields);
            _context.DemandsNames.RemoveRange(names);
            _context.DemandsNameDocuments.RemoveRange(nameDocuments);
            _context.DemandStatuses.RemoveRange(statuses);
            _context.Demands.Remove(demand);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/Demands/next-regn/{demandCode}
        [HttpGet("next-regn/{demandCode}")]
        public async Task<ActionResult<object>> GetNextRegN(string demandCode)
        {
            var currentYear = DateTime.Now.Year;
            var maxRegN = await _context.Demands
                .Where(d => d.DemandCode == demandCode && d.YearDate == currentYear)
                .MaxAsync(d => (int?)d.RegN) ?? 0;

            return Ok(new { 
                demandCode = demandCode,
                yearDate = currentYear,
                nextRegN = maxRegN + 1
            });
        }

        // GET: api/Demands/demand-types
        [HttpGet("demand-types")]
        public async Task<ActionResult<IEnumerable<object>>> GetDemandTypes()
        {
            var demandTypes = await _context.DemandsNames
                .AsNoTracking()
                .OrderBy(d => d.RowId)
                .Select(d => new {
                    rowId = d.RowId,
                    demandTypeId = d.DemandTypeId,
                    demandName = d.DemandName,
                    demandCode = d.DemandCode,
                    docFileName = d.DocFileName,
                    proccesPeriod = d.ProccesPeriod
                })
                .ToListAsync();

            return Ok(demandTypes);
        }

        // GET: api/Demands/types
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<object>>> GetTypes()
        {
            var types = await _context.DemandsNames
                .AsNoTracking()
                .OrderBy(d => d.RowId)
                .Select(d => new {
                    demandsNameId = d.RowId,
                    demandNameAr = d.DemandName ?? "",
                    deadlineDays = d.ProccesPeriod ?? 15
                })
                .ToListAsync();

            return Ok(types);
        }

        // POST: api/Demands/types
        [HttpPost("types")]
        public async Task<ActionResult<object>> CreateDemandType([FromBody] System.Text.Json.JsonElement dto)
        {
            string? demandNameAr = null;
            int? deadlineDays = 15;
            string? createdBy = "web";

            if (dto.TryGetProperty("demandNameAr", out var demandNameArProp))
                demandNameAr = demandNameArProp.GetString();
            if (dto.TryGetProperty("deadlineDays", out var deadlineDaysProp))
                deadlineDays = deadlineDaysProp.GetInt32();
            if (dto.TryGetProperty("createdBy", out var createdByProp))
                createdBy = createdByProp.GetString();

            int newRowId = 1;
            var maxRowId = await _context.DemandsNames.MaxAsync(d => (int?)d.RowId);
            if (maxRowId.HasValue)
                newRowId = maxRowId.Value + 1;

            var demandName = new DemandsName
            {
                RowId = newRowId,
                DemandName = demandNameAr ?? string.Empty,
                ProccesPeriod = deadlineDays ?? 15,
                UpdatedBy = createdBy ?? "web",
                LastUpdated = DateTime.Now,
                RowVersion = 0,
                TtimeStamp = new byte[8]
            };
            
            _context.DemandsNames.Add(demandName);
            await _context.SaveChangesAsync();
            
            return Ok(new {
                demandsNameId = demandName.RowId,
                demandNameAr = demandName.DemandName,
                deadlineDays = demandName.ProccesPeriod
            });
        }

        // GET: api/Demands/byRegNumber/{regNumber}
        // ✅ FIXED: Improved parsing logic to handle various registration number formats
        // Supports formats like: ش2025/123, م2024/456, 2025/789
        [HttpGet("byRegNumber/{regNumber}")]
        public async Task<ActionResult<object>> GetByRegNumber(string regNumber)
        {
            try
            {
                // Split by '/'
                var parts = regNumber.Split('/');
                if (parts.Length != 2)
                {
                    return BadRequest(new
                    {
                        message = "Invalid registration number format. Expected format: CODE+YEAR/NUMBER (e.g., ش2025/123)"
                    });
                }

                var yearPart = parts[0].Trim();
                var numPart = parts[1].Trim();

                // Extract code and year
                string code = "";
                string yearStr = yearPart;

                // Check if first character(s) are non-numeric (demand code)
                int yearStartIndex = 0;
                for (int i = 0; i < yearPart.Length; i++)
                {
                    if (char.IsDigit(yearPart[i]))
                    {
                        yearStartIndex = i;
                        break;
                    }
                }

                if (yearStartIndex > 0)
                {
                    code = yearPart.Substring(0, yearStartIndex);
                    yearStr = yearPart.Substring(yearStartIndex);
                }

                // Parse year and registration number
                if (!int.TryParse(yearStr, out int year))
                {
                    return BadRequest(new
                    {
                        message = $"Invalid year format: '{yearStr}'. Expected numeric year."
                    });
                }

                if (!int.TryParse(numPart, out int regN))
                {
                    return BadRequest(new
                    {
                        message = $"Invalid registration number: '{numPart}'. Expected numeric value."
                    });
                }

                // Query the database
                var demand = await _context.Demands
                    .Include(d => d.CallerNameNavigation) // Include person for caller name
                    .AsNoTracking()
                    .Where(d => d.RegN == regN && d.YearDate == year && d.DemandCode == code)
                    .Select(d => new
                    {
                        demandId = d.DemandId,
                        regN = $"{d.DemandCode}{d.YearDate}/{d.RegN}",
                        regCode = d.DemandCode,
                        yearDate = d.YearDate,
                        registrationNumber = d.RegN,
                        title = d.Title,
                        applicantName = d.CallerDesc,
                        callerName = d.CallerNameNavigation != null
                            ? $"{d.CallerNameNavigation.PersonFname} {d.CallerNameNavigation.PersonMname} {d.CallerNameNavigation.PersonLname}".Trim()
                            : d.CallerDesc,
                        trxnDate = d.TrxnDate,
                        valueDate = d.ValueDate,
                        completedDate = d.CompletionDate,
                        status = d.FinalStatus,
                        realEstate = d.RealEstate,
                        block = d.Block,
                        part = d.Part,
                        floor = d.Floor,
                        hay = d.Hay,
                        near = d.Near,
                        mantaka = d.Realestatearea,
                        result = d.ResultExp,
                        notes = d.Note2,
                        pdfSourcePath = d.PdfSourcePath
                    })
                    .FirstOrDefaultAsync();

                if (demand == null)
                {
                    return NotFound(new
                    {
                        message = $"Demand not found for registration number: {regNumber}",
                        searchCriteria = new
                        {
                            code,
                            year,
                            regN
                        }
                    });
                }

                return Ok(demand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "خطأ في البحث عن الطلب",
                    error = ex.Message
                });
            }
        }

        // GET: api/Demands/nextRegNumber
        [HttpGet("nextRegNumber")]
        public async Task<ActionResult<object>> GetNextRegNumber([FromQuery] string type = "م")
        {
            var currentYear = DateTime.Now.Year;
            var maxRegN = await _context.Demands
                .Where(d => d.DemandCode == type && d.YearDate == currentYear)
                .MaxAsync(d => (int?)d.RegN) ?? 0;

            return Ok(new { regN = $"{currentYear}/{maxRegN + 1}" });
        }

        // ============================================================
        // GET: api/Demands/statistics
        // ✅ FIXED: completed count now checks multiple status values
        //    AND checks CompletionDate (not null and not 1900-01-01)
        // ============================================================
        [HttpGet("statistics")]
        public async Task<ActionResult<object>> GetStatistics([FromQuery] int? userId = null)
        {
            var total = await _context.Demands.CountAsync();

            // Fix: check multiple possible "completed" status values
            // and also check if CompletionDate is meaningfully set
            var completedStatuses = new[] { "منجز", "منجزة", "مكتمل", "مكتملة", "completed", "done" };
            var minDate = new DateTime(1900, 1, 2);

            var completed = await _context.Demands.CountAsync(d =>
                (d.FinalStatus != null && completedStatuses.Contains(d.FinalStatus)) ||
                (d.CompletionDate != null && d.CompletionDate > minDate)
            );

            var pending = await _context.Demands.CountAsync(d =>
                d.FinalStatus == "قيد الدرس" || d.FinalStatus == null || d.FinalStatus == ""
            );

            var complaints = await _context.Demands.CountAsync(d => d.DemandCode == "ش");
            var pendingIhalers = await _context.Ihalers.CountAsync(i =>
                i.IhalerStatus == null || i.IhalerStatus == "قيد الدرس"
            );

            int myReferrals = 0, myCompleted = 0, myPendingIhalers = 0, myPending = 0;
            if (userId.HasValue)
            {
                myReferrals = await _context.Ihalers.CountAsync(i => i.AppGroupId == userId.Value);
                myCompleted = await _context.Demands.CountAsync(d =>
                    d.UpdatedBy == userId.ToString() &&
                    ((d.FinalStatus != null && completedStatuses.Contains(d.FinalStatus)) ||
                     (d.CompletionDate != null && d.CompletionDate > minDate))
                );
                myPendingIhalers = await _context.Ihalers.CountAsync(i =>
                    i.AppGroupId == userId.Value && (i.IhalerStatus == null || i.IhalerStatus == "قيد الدرس")
                );
                myPending = await _context.Demands.CountAsync(d =>
                    d.UpdatedBy == userId.ToString() &&
                    (d.FinalStatus == "قيد الدرس" || d.FinalStatus == null || d.FinalStatus == "")
                );
            }

            return Ok(new {
                total,
                completed,
                pending,
                complaints,
                pendingIhalers,
                myReferrals,
                myCompleted,
                myPendingIhalers,
                myPending
            });
        }

        // ============================================================
        // GET: api/Demands/statuses
        // ✅ NEW: Returns distinct FinalStatus values for dropdowns
        // ============================================================
        [HttpGet("statuses")]
        public async Task<ActionResult<IEnumerable<string>>> GetDistinctStatuses()
        {
            var statuses = await _context.Demands
                .AsNoTracking()
                .Where(d => d.FinalStatus != null && d.FinalStatus != "")
                .Select(d => d.FinalStatus!)
                .Distinct()
                .OrderBy(s => s)
                .ToListAsync();

            return Ok(statuses);
        }

    }
}
