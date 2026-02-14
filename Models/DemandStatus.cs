using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class DemandStatus
{
    internal int DemandId;

    public int StatusId { get; set; }

    public string? StatusDesc { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[]? TtimeStamp { get; set; }
}
