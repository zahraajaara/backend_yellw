using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class GroupMenu
{
    public int GroupMenuId { get; set; }

    public int AppGroupId { get; set; }

    public string MenuName { get; set; } = null!;

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[]? TtimeStamp { get; set; }
}
