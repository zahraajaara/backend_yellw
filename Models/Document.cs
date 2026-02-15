using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class Document
{
    public int DocumentId { get; set; }

    public string? DocumentName { get; set; }

    public string? IsReq { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[] TtimeStamp { get; set; } = null!;
}
