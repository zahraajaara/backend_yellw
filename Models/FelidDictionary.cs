using System;
using System.Collections.Generic;

namespace YellowKalam.Api.Models;

public partial class FelidDictionary
{
    public int FieldId { get; set; }

    public string? FieldName { get; set; }

    public string? FieldObjectName { get; set; }

    public string? FieldCaprionName { get; set; }

    public string? FieldCaprionTxt { get; set; }

    public decimal? FieldFather { get; set; }

    public int? FieldOrder { get; set; }

    public int? FieldLength { get; set; }

    public string? FieldReplacement { get; set; }

    public string? IsRequired { get; set; }

    public int? Ord { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public byte[] TtimeStamp { get; set; } = null!;
}
