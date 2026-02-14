using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class DemandsNameDocument
{
    internal int DemandId;

    public int RowId { get; set; }

    public int DocumentId { get; set; }

    public string? IsAdd { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[] TtimeStamp { get; set; } = null!;

    public virtual DemandsName Row { get; set; } = null!;
}
