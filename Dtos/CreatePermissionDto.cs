namespace YellowKalam.Api.Dtos
{
    public class CreatePermissionDto
    {
        public int AppUserId { get; set; }
        public string Permission { get; set; } = string.Empty;
    }

}
