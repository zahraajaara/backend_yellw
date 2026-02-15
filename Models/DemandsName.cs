using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class DemandsName
{
    internal int DemandId;

    public int RowId { get; set; }

    public int? DemandTypeId { get; set; }

    public string? DemandName { get; set; }

    public string? DemandCode { get; set; }

    public string? DocFileName { get; set; }

    public int? ProccesPeriod { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[] TtimeStamp { get; set; } = null!;

    public virtual ICollection<Demand> Demands { get; set; } = new List<Demand>();

    public virtual ICollection<DemandsNameDocument> DemandsNameDocuments { get; set; } = new List<DemandsNameDocument>();
}
