using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class AppGroup
{
    public int AppGroupId { get; set; }

    public string AppGroupName { get; set; } = null!;

    public string IsAdministrator { get; set; } = null!;

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[]? TtimeStamp { get; set; }
}
