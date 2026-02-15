namespace YellowKalam.Api.Dtos
{
    public class CreatePersonDto
    {
        public int? AlSifaId { get; set; }

        public string? PersonFname { get; set; }
        public string? PersonMname { get; set; }
        public string? PersonLName { get; set; }
        public string? PersonMoName { get; set; }

        public string? Ref1 { get; set; }
        public string? Ref2 { get; set; }
        public string? Ref3 { get; set; }

        public string? PersonGender { get; set; }
        public DateTime? PersonDoB { get; set; }

        public string? PersonRegPlace { get; set; }
        public string? PersonRegNum { get; set; }
        public string? PersonNationId { get; set; }

        public int? PersonTypeId { get; set; }
        public DateTime? PersonDateOfIssue { get; set; }

        public string? PersonCardNum { get; set; }
        public int? PersonMaritalStatusId { get; set; }

        public string? TaxNum { get; set; }
        public string? TvaNum { get; set; }
        public string? BRegNum { get; set; }

        public string? CortAndY { get; set; }
        public string? Reference { get; set; }
        public string? Notes { get; set; }
    }
}
