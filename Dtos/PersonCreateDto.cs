namespace YellowKalam.Api.Dtos
{
    public class PersonCreateDto
    {
        public int AlSifaId { get; set; }
        public string PersonFname { get; set; } = null!;
        public string? PersonMname { get; set; }
        public string? PersonLname { get; set; }
        public string? PersonMoName { get; set; }
        public string? Ref1 { get; set; }
        public string? Ref2 { get; set; }
        public string? Ref3 { get; set; }
        public string PersonGender { get; set; } = null!;
        public int PersonNationId { get; set; }
        public int PersonTypeId { get; set; }
        public int PersonMaritalStatusId { get; set; }
        public string? TaxNum { get; set; }
        public string? Tvanum { get; set; }
        public string? BregNum { get; set; }
        public string? CortAndY { get; set; }
        public string? Reference { get; set; }
        public string? Notes { get; set; }
    }

}
