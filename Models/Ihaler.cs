using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class Ihaler
{
    public int IhalerId { get; set; }

    public int? DemandId { get; set; }

    public int? IhalerNum { get; set; }

    public int? AppGroupId { get; set; }

    public DateTime? IhalerDate { get; set; }

    public int? IhalerTypeId { get; set; }

    public string? IhalerStatus { get; set; }

    public DateTime? IhalerRevissionDate { get; set; }

    public string? KarrarNum { get; set; }

    public DateTime? KarrarDate { get; set; }

    public string? IhalerDesc { get; set; }

    public DateTime? ReceivedDate { get; set; }

    public byte[]? IhalerSign { get; set; }

    public string? IsLast { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[]? TtimeStamp { get; set; }

    public virtual Demand? Demand { get; set; }
}
