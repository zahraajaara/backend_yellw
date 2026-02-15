using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class LastId
{
    public string TableName { get; set; } = null!;

    public int LastId1 { get; set; }

    public DateTime LastUpdated { get; set; }

    public byte[]? TtimeStamp { get; set; }
}
