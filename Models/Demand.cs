using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YellowKalam.Api.Models;

public partial class Demand
{
    public int DemandId { get; set; }

    public int? RegN { get; set; }

    public string DemandCode { get; set; } = null!;

    public int YearDate { get; set; }

    public int? RowId { get; set; }

    public int? ArticleCategoryId { get; set; }

    public string? Title { get; set; }
    
    [NotMapped] // May not exist in database
    public string? Signature { get; set; }

    public int? CallerName { get; set; }

    public string? CallerDesc { get; set; }

    public string? CallerJob { get; set; }

    public int? Applicant { get; set; }

    public string? ApplicantDesc { get; set; }

    public string? ApplicantJob { get; set; }

    public DateTime? TrxnDate { get; set; }

    public DateTime? ValueDate { get; set; }

    public DateTime? CompletionDate { get; set; }

    public string? TrxnSubject { get; set; }

    public string? ResultExp { get; set; }

    public string? DetectionNum { get; set; }

    public string? RealEstate { get; set; }

    public string? Part { get; set; }

    public string? Floor { get; set; }

    public int? OwnerName { get; set; }

    public int? Renter { get; set; }

    public string? MsalePrice { get; set; }

    public string? Amount { get; set; }

    public string? Realestatearea { get; set; }


    public string? Note2 { get; set; }

    public string? Note3 { get; set; }

    public string? Ref1 { get; set; }

    public string? Ref2 { get; set; }

    public string? Ref3 { get; set; }

    public string? Ref4 { get; set; }

    public string? Ref5 { get; set; }

    public string? Ref6 { get; set; }

    public string? Ref7 { get; set; }

    public string? Ref8 { get; set; }

    public string? Ref9 { get; set; }

    public string? PdfSourcePath { get; set; }

    public DateTime? ReceivedDate { get; set; }

    public string? FinalStatus { get; set; }

    public string? Block { get; set; }

    public string? CallerIs { get; set; }

    public string? Hay { get; set; }

    public string? Near { get; set; }

    public string? Stype { get; set; }

    public int RowVersion { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime LastUpdated { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public byte[]? TtimeStamp { get; set; }

    public virtual Person? CallerNameNavigation { get; set; }

    public virtual ICollection<Ihaler> Ihalers { get; set; } = new List<Ihaler>();

    public virtual DemandsName? Row { get; set; }
}
