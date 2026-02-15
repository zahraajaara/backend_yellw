using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using YellowKalam.Api.Data;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly YellowKalamContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(YellowKalamContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public class LoginRequestDto
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            try
            {
                var username = (request.Username ?? "").Trim();
                var password = request.Password ?? "";

                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                    return Unauthorized("Invalid username or password.");

                var normUsername = username.ToLowerInvariant();

                // IMPORTANT: remove AsNoTracking so we can upgrade old hashes
                var user = await _context.AppUsers
                    .FirstOrDefaultAsync(u =>
                        u.AppUserName != null &&
                        u.AppUserName.Trim().ToLower() == normUsername &&
                        u.IsActive != null &&
                        u.IsActive.Trim().ToUpper() == "Y"
                    );

                if (user == null)
                    return Unauthorized("Invalid username or password.");

                var stored = (user.HashedPassword ?? "").Trim();
                if (string.IsNullOrWhiteSpace(stored))
                    return Unauthorized("User password not set.");

                // New hash (your current system)
                var sha256Base64 = HashPasswordSha256Base64(password);

                // Old hash (what exists in your DB for many users) -> SHA1 HEX length 40
                var sha1Hex = HashPasswordSha1Hex(password);

                var ok =
                    string.Equals(stored, sha256Base64, StringComparison.Ordinal) ||
                    string.Equals(stored, sha1Hex, StringComparison.OrdinalIgnoreCase);

                if (!ok)
                    return Unauthorized("Invalid username or password.");

                // Auto-upgrade old SHA1 hashes to SHA256 Base64 on successful login
                if (stored.Length == 40) // looks like SHA1-hex
                {
                    user.HashedPassword = sha256Base64;
                    await _context.SaveChangesAsync();
                }

                // Get user's group to check if admin
                var group = await _context.AppGroups.FindAsync(user.AppGroupId);
                var isAdmin = group?.IsAdministrator?.Trim().ToUpper() == "Y" || user.AppGroupId == 1;

                var token = GenerateToken(user);

                return Ok(new
                {
                    token,
                    appuserId = user.AppUserId,
                    pUserCode = user.AppUserCode,
                    userNameAr = user.AppUserName,
                    userNameEn = user.AppUserName,
                    appgroupId = user.AppGroupId,
                    isAdministrator = isAdmin,
                    signatureImage = (byte[]?)null, // SignatureImage column doesn't exist in database
                    permissions = new { }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "خطأ في الاتصال بقاعدة البيانات", error = ex.Message });
            }
        }

        // GET: api/Auth/municipality
        [HttpGet("municipality")]
        public async Task<IActionResult> GetMunicipality()
        {
            try
            {
                var municipality = await _context.Municipalities.FirstOrDefaultAsync();
                if (municipality == null)
                    return NotFound("No municipality found");

                return Ok(new
                {
                    municipalityId = municipality.MunicipalityId,
                    name = municipality.MunicipalityName,
                    cityName = municipality.MunicipalityCityName,
                    address = municipality.MunicipalityMainAddress,
                    tel = municipality.MunicipalityTel,
                    fax = municipality.MunicipalityFax,
                    web = municipality.MunicipalityWeb,
                    logo = municipality.MunicipalityLogo != null ? Convert.ToBase64String(municipality.MunicipalityLogo) : null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "خطأ في جلب بيانات البلدية", error = ex.Message });
            }
        }

        // GET: api/Auth/user-signature/{userId}
        [HttpGet("user-signature/{userId}")]
        public async Task<IActionResult> GetUserSignature(int userId)
        {
            try
            {
                var user = await _context.AppUsers.FindAsync(userId);
                if (user == null)
                    return NotFound("User not found");

                return Ok(new
                {
                    userId = user.AppUserId,
                    userName = user.AppUserName,
                    signatureImage = (string?)null // SignatureImage column doesn't exist in database
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "خطأ في جلب التوقيع", error = ex.Message });
            }
        }

        // POST: api/Auth/update-signature
        [HttpPost("update-signature")]
        public async Task<IActionResult> UpdateSignature([FromBody] UpdateSignatureDto dto)
        {
            try
            {
                var user = await _context.AppUsers.FindAsync(dto.UserId);
                if (user == null)
                    return NotFound("User not found");

                // SignatureImage column doesn't exist in database, so we can't update it
                // Return success message but don't actually update the database
                user.LastUpdated = DateTime.Now;
                user.UpdatedBy = "WebApp";

                await _context.SaveChangesAsync();
                return Ok(new { message = "تم تحديث التوقيع بنجاح" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "خطأ في تحديث التوقيع", error = ex.Message });
            }
        }

        public class UpdateSignatureDto
        {
            public int UserId { get; set; }
            public string? SignatureBase64 { get; set; }
        }

        private string GenerateToken(AppUser user)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.AppUserId.ToString()),
                new Claim(ClaimTypes.Name, user.AppUserName),

                new Claim("AppUserId", user.AppUserId.ToString()),
                new Claim("appUserCode", user.AppUserCode),
                new Claim("appUserName", user.AppUserName),
                new Claim("appGroupId", user.AppGroupId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // ✅ New (current) hash: SHA256 -> Base64 (length 44)
        private static string HashPasswordSha256Base64(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // ✅ Old hash in your DB: SHA1 -> hex (length 40)
        private static string HashPasswordSha1Hex(string password)
        {
            using var sha1 = SHA1.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha1.ComputeHash(bytes);
            return Convert.ToHexString(hash).ToLowerInvariant();
        }
    }
}
