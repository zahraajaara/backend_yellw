using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YellowKalam.Api.Models
{
    [Table("Permissions")]
    public class Permission
    {
        [Column("AppUserID")]
        public int AppUserId { get; set; }

        [Required]
        [Column("Permission")]
        [MaxLength(200)]
        public string PermissionName { get; set; } = string.Empty;

        // optional navigation (not required)
        public AppUser? AppUser { get; set; }
    }
}
