using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class CaptionChange
{
    public int RowId { get; set; }

    public string? Note1 { get; set; }

    public string? Note2 { get; set; }

    public string? Note3 { get; set; }

    public string? Ref1 { get; set; }

    public string? Ref2 { get; set; }

    public string? Ref3 { get; set; }

    public string? Ref4 { get; set; }

    public string? Ref5 { get; set; }

    public string? Ref6 { get; set; }

    public string? Ref7 { get; set; }

    public string? Ref8 { get; set; }

    public string? Ref9 { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[] TtimeStamp { get; set; } = null!;
}
