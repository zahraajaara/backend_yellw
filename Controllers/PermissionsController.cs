using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Data;
using YellowKalam.Api.Dtos;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "PermissionsAccess")] // ✅ allow 1 and 2048
    public class PermissionsController : ControllerBase
    {
        private readonly YellowKalamContext _context;

        public PermissionsController(YellowKalamContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Permissions
                .AsNoTracking()
                .OrderBy(p => p.AppUserId)
                .ThenBy(p => p.PermissionName)
                .Select(p => new
                {
                    appUserId = p.AppUserId,
                    permission = p.PermissionName
                })
                .ToListAsync();

            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePermissionDto dto)
        {
            if (dto == null || dto.AppUserId <= 0 || string.IsNullOrWhiteSpace(dto.Permission))
                return BadRequest("Invalid payload.");

            var userExists = await _context.AppUsers.AnyAsync(u => u.AppUserId == dto.AppUserId);
            if (!userExists)
                return BadRequest("User does not exist.");

            var perm = dto.Permission.Trim();

            var exists = await _context.Permissions.AnyAsync(p =>
                p.AppUserId == dto.AppUserId &&
                p.PermissionName == perm);

            if (exists)
                return Conflict("Permission already exists.");

            _context.Permissions.Add(new Permission
            {
                AppUserId = dto.AppUserId,
                PermissionName = perm
            });

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{appUserId:int}/{permission}")]
        public async Task<IActionResult> Delete(int appUserId, string permission)
        {
            if (appUserId <= 0 || string.IsNullOrWhiteSpace(permission))
                return BadRequest("Invalid route values.");

            var entity = await _context.Permissions.FirstOrDefaultAsync(p =>
                p.AppUserId == appUserId &&
                p.PermissionName == permission);

            if (entity == null)
                return NotFound();

            _context.Permissions.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
