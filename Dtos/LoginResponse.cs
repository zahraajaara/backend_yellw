namespace YellowKalam.Api.Dtos
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;

        public int AppuserId { get; set; }
        public string PUserCode { get; set; } = string.Empty;
        public string UserNameAr { get; set; } = string.Empty;
        public string UserNameEn { get; set; } = string.Empty;
        public int AppgroupId { get; set; }
        public int? BranchId { get; set; }
        public bool IsAdministrator { get; set; }
    }
}
