using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class AppUser
{
    public int AppUserId { get; set; }

    public int AppGroupId { get; set; }

    public string AppUserCode { get; set; } = null!;

    public string AppUserName { get; set; } = null!;

    public string IsGroupLeader { get; set; } = null!;

    public int BranchId { get; set; }

    public string LimitToBranch { get; set; } = null!;

    public string HashedPassword { get; set; } = null!;

    public DateTime LastLoginDate { get; set; }

    public DateTime LastPwdChangeDt { get; set; }

    public string LastHostUsed { get; set; } = null!;

    public string IsActive { get; set; } = null!;

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[] TtimeStamp { get; set; } = null!;

    // User signature image (base64 or path)
    public byte[]? SignatureImage { get; set; }
}
