using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class DemandsField
{
    internal int DemandId;

    public int RowId { get; set; }

    public int FieldId { get; set; }

    public decimal? SortNum { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[] TtimeStamp { get; set; } = null!;
}
