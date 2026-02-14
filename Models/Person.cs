using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public int AlSifaId { get; set; }

    public string PersonFname { get; set; } = null!;

    public string? PersonMname { get; set; }

    public string? PersonLname { get; set; }

    public string? PersonMoName { get; set; }

    public string? Ref1 { get; set; }

    public string? Ref2 { get; set; }

    public string? Ref3 { get; set; }

    public string PersonGender { get; set; } = null!;

    public DateTime PersonDoB { get; set; }

    public string? PersonRegPlace { get; set; }

    public string? PersonRegNum { get; set; }

    public int PersonNationId { get; set; }

    public int PersonTypeId { get; set; }

    public DateTime PersonDateOfIssue { get; set; }

    public string? PersonCardNum { get; set; }

    public int PersonMaritalStatusId { get; set; }

    public string? TaxNum { get; set; }

    public string? Tvanum { get; set; }

    public string? BregNum { get; set; }

    public string? CortAndY { get; set; }

    public string? Reference { get; set; }

    public string? Notes { get; set; }

    public byte[]? PersonImage { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }   // keep required (server sets it)

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }     // keep required (server sets it)

    public byte[]? TtimeStamp { get; set; }

    public virtual ICollection<Demand> Demands { get; set; } = new List<Demand>();
}
