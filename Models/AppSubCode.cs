using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class AppSubCode
{
    public int AppSubCodeId { get; set; }

    public string AppSubCodeCode { get; set; } = null!;

    public int AppCodeId { get; set; }

    public string AppSubCodeName { get; set; } = null!;

    public string AppSubCodeAname { get; set; } = null!;

    public string AppSubCodeDescription { get; set; } = null!;

    public string IsDefault { get; set; } = null!;

    public int? Id { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public byte[]? TtimeStamp { get; set; }
}
