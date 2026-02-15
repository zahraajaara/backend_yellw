using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourProjectNamespace.Data;    // YellowKalamContext
using YourProjectNamespace.Models;  // AppUser, AppGroup, SystemSettings

namespace YourProjectNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // adjust if you use different role logic
    public class AdminController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public AdminController(YellowKalamContext context)
        {
            _context = context;
        }

        #region DTOs

        public class AppUserDto
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public bool IsActive { get; set; }
            public int? GroupId { get; set; }
            public string GroupName { get; set; }
        }

        public class CreateUserDto
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            public string Password { get; set; }

            public string FullName { get; set; }
            public string Email { get; set; }
            public bool IsActive { get; set; } = true;
            public int? GroupId { get; set; }
        }

        public class UpdateUserDto
        {
            [Required]
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public bool IsActive { get; set; }
            public int? GroupId { get; set; }

            // Optional password change
            public string NewPassword { get; set; }
        }

        public class SettingsDto
        {
            public string SystemName { get; set; }
            public string DefaultLanguage { get; set; }
            public string SmsSenderName { get; set; }
            public string DefaultBranchCode { get; set; }
        }

        #endregion

        // GET: api/admin/users
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<AppUserDto>>> GetUsers()
        {
            var users = await _context.AppUsers
                .Include(u => u.Group) // navigation property
                .AsNoTracking()
                .OrderBy(u => u.UserName)
                .Select(u => new AppUserDto
                {
                    Id = u.AppUserId, // adjust if your PK is different
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    GroupId = u.GroupId,
                    GroupName = u.Group != null ? u.Group.AppGroupName : null
                })
                .ToListAsync();

            return Ok(users);
        }

        // POST: api/admin/users
        [HttpPost("users")]
        public async Task<ActionResult<AppUserDto>> CreateUser([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var passwordHash = HashPassword(dto.Password);

            var user = new AppUser
            {
                UserName = dto.UserName,
                PasswordHash = passwordHash,  // property name in your model
                FullName = dto.FullName,
                Email = dto.Email,
                IsActive = dto.IsActive,
                GroupId = dto.GroupId
            };

            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();

            var result = new AppUserDto
            {
                Id = user.AppUserId,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                IsActive = user.IsActive,
                GroupId = user.GroupId,
                GroupName = null
            };

            return CreatedAtAction(nameof(GetUsers), new { id = user.AppUserId }, result);
        }

        // PUT: api/admin/users/{id}
        [HttpPut("users/{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.AppUsers.FindAsync(id);
            if (user == null)
                return NotFound();

            user.UserName = dto.UserName;
            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.IsActive = dto.IsActive;
            user.GroupId = dto.GroupId;

            if (!string.IsNullOrWhiteSpace(dto.NewPassword))
            {
                user.PasswordHash = HashPassword(dto.NewPassword);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/admin/settings
        [HttpGet("settings")]
        public async Task<ActionResult<SettingsDto>> GetSettings()
        {
            // Assumes a single settings row; adjust if you store differently
            var settings = await _context.SystemSettings
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (settings == null)
            {
                return Ok(new SettingsDto());
            }

            return Ok(new SettingsDto
            {
                SystemName = settings.SystemName,
                DefaultLanguage = settings.DefaultLanguage,
                SmsSenderName = settings.SmsSenderName,
                DefaultBranchCode = settings.DefaultBranchCode
            });
        }

        // PUT: api/admin/settings
        [HttpPut("settings")]
        public async Task<IActionResult> UpdateSettings([FromBody] SettingsDto dto)
        {
            var settings = await _context.SystemSettings.FirstOrDefaultAsync();

            if (settings == null)
            {
                settings = new SystemSettings();
                _context.SystemSettings.Add(settings);
            }

            settings.SystemName = dto.SystemName;
            settings.DefaultLanguage = dto.DefaultLanguage;
            settings.SmsSenderName = dto.SmsSenderName;
            settings.DefaultBranchCode = dto.DefaultBranchCode;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Simple hashing example – replace with your real logic
        private static string HashPassword(string password)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha.ComputeHash(bytes);
                return System.Convert.ToBase64String(hash);
            }
        }
    }
}
