using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class Article
{
    public int ArticleId { get; set; }

    public int? ArticleCategoryId { get; set; }

    public string? ArticleText { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[]? TtimeStamp { get; set; }
}
