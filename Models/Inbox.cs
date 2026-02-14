using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class Inbox
{
    public int RowId { get; set; }

    public int? WelayaId { get; set; }

    public string? Subject { get; set; }

    public string? Comments { get; set; }

    public string? SourceName { get; set; }

    public string? AppUserNameFrom { get; set; }

    public string? AppUserNameTo { get; set; }

    public string? AppUserInboxs { get; set; }

    public DateTime? LastUpdated { get; set; }

    public int? RowVersion { get; set; }
}
