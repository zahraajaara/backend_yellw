using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class Municipality
{
    public int MunicipalityId { get; set; }

    public int UnionId { get; set; }

    public int? MinistryId { get; set; }

    public int? CazaId { get; set; }

    public string? MunicipalityName { get; set; }

    public string? MunicipalityCityName { get; set; }

    public string? MunicipalityMainAddress { get; set; }

    public string? MunicipalityTel { get; set; }

    public string? MunicipalityFax { get; set; }

    public string? MunicipalityWeb { get; set; }

    public byte[]? MunicipalityLogo { get; set; }

    public DateTime? DateOfBorn { get; set; }

    public string? Ref1 { get; set; }

    public string? Ref2 { get; set; }

    public int? RowVersion { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? LastUpdated { get; set; }

    public byte[] TtimeStamp { get; set; } = null!;
}
