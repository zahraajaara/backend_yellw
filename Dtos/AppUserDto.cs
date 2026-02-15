namespace YellowKalam.Api.Dtos
{
    public class AppUserDto
    {
        public int AppUserId { get; set; }
        public int AppGroupId { get; set; }
        public string AppUserCode { get; set; } = string.Empty;
        public string AppUserName { get; set; } = string.Empty;
        public string IsGroupLeader { get; set; } = "N";
        public string IsActive { get; set; } = "Y";
        public string? HashedPassword { get; set; }
    }

    public class CreateAppUserDto
    {
        public int AppGroupId { get; set; }
        public string AppUserCode { get; set; } = string.Empty;
        public string AppUserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // plain password
        public string IsGroupLeader { get; set; } = "N";
        public string IsActive { get; set; } = "Y";
    }

    public class UpdateAppUserDto
    {
        public int AppUserId { get; set; }
        public int AppGroupId { get; set; }
        public string AppUserCode { get; set; } = string.Empty;
        public string AppUserName { get; set; } = string.Empty;
        public string IsGroupLeader { get; set; } = "N";
        public string IsActive { get; set; } = "Y";
    }
}
