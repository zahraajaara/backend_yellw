using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class BulkSm
{
    public int Smsid { get; set; }

    public string? Phones { get; set; }

    public string? Sms { get; set; }

    public int? RowVersion { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? LastUpdated { get; set; }

    public byte[] TtimeStamp { get; set; } = null!;
}
