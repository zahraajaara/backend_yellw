using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class Branch
{
    public int BranchId { get; set; }

    public int? CompanyId { get; set; }

    public string BranchName { get; set; } = null!;

    public string? BranchAddress { get; set; }

    public string? BranchTel { get; set; }

    public string? BranchFax { get; set; }

    public string? BranchDescrition { get; set; }

    public string IsAmainBranch { get; set; } = null!;

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[]? TtimeStamp { get; set; }
}
