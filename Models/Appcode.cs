using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class Appcode
{
    public int AppcodeId { get; set; }

    public string AppCodeCode { get; set; } = null!;

    public int AppcodeNameId { get; set; }

    public string AppcodeName { get; set; } = null!;

    public string AppcodeAname { get; set; } = null!;

    public string? AppcodeDescription { get; set; }

    public string IsDefault { get; set; } = null!;

    public string HasChild { get; set; } = null!;

    public int? Id { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public byte[]? TtimeStampa { get; set; }
}
