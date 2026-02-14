using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class AppCodeName
{
    public int AppCodeNameId { get; set; }

    public string? AppCodeNameCode { get; set; }

    public string AppCodeNameName { get; set; } = null!;

    public string AppcodeNameAname { get; set; } = null!;

    public string AppcodeNameDescription { get; set; } = null!;

    public string ShowArabicInCombos { get; set; } = null!;

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[]? TtimeStamp { get; set; }
}
