using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class Ministry
{
    public int MinistryId { get; set; }

    public string? MinistryName { get; set; }

    public string? MinistryAddress { get; set; }

    public string? MinistryPhone { get; set; }

    public string? MinistryWeb { get; set; }

    public byte[]? MinistrryLogo { get; set; }

    public int? RowVersion { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? LastUpdated { get; set; }

    public byte[] STtimeStamp { get; set; } = null!;
}
