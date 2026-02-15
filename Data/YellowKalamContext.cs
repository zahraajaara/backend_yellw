using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using YellowKalam.Api.Models;

namespace YellowKalam.Api.Data;

public partial class YellowKalamContext : DbContext
{
    public YellowKalamContext()
    {
    }

    public YellowKalamContext(DbContextOptions<YellowKalamContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Ehsaa> Ehsaas { get; set; }

    public virtual DbSet<AppCodeName> AppCodeNames { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<AppGroup> AppGroups { get; set; }

    public virtual DbSet<AppSubCode> AppSubCodes { get; set; }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<Appcode> Appcodes { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Branch> Branches { get; set; }

    public virtual DbSet<BulkSm> BulkSms { get; set; }
    public virtual DbSet<Kashef> Kashef { get; set; } = null!;

    public virtual DbSet<CaptionChange> CaptionChanges { get; set; }

    public virtual DbSet<Demand> Demands { get; set; }

    public virtual DbSet<DemandAvailableDoc> DemandAvailableDocs { get; set; }

    public virtual DbSet<DemandStatus> DemandStatuses { get; set; }

    public virtual DbSet<DemandsField> DemandsFields { get; set; }

    public virtual DbSet<DemandsName> DemandsNames { get; set; }

    public virtual DbSet<DemandsNameDocument> DemandsNameDocuments { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<FelidDictionary> FelidDictionaries { get; set; }

    public virtual DbSet<GroupMenu> GroupMenus { get; set; }

    public virtual DbSet<Ihaler> Ihalers { get; set; }

    public virtual DbSet<Inbox> Inboxs { get; set; }

    public virtual DbSet<LastId> LastIds { get; set; }

    public virtual DbSet<Ministry> Ministries { get; set; }

    public virtual DbSet<Municipality> Municipalities { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<Referencess> Referencesses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       

        base.OnModelCreating(modelBuilder);
        modelBuilder.UseCollation("Arabic_CI_AI");

        modelBuilder.Entity<AppCodeName>(entity =>
        {
            entity.ToTable("AppCodeName");

            entity.Property(e => e.AppCodeNameId).ValueGeneratedNever();
            entity.Property(e => e.AppCodeNameCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.AppCodeNameName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.AppcodeNameAname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("AppcodeNameAName");
            entity.Property(e => e.AppcodeNameDescription)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.ShowArabicInCombos)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.TtimeStamp)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });

        modelBuilder.Entity<AppGroup>(entity =>
        {
            entity.ToTable("AppGroup");

            entity.Property(e => e.AppGroupId)
                .ValueGeneratedNever()
                .HasColumnName("AppGroupID");
            entity.Property(e => e.AppGroupName)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.IsAdministrator)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.TtimeStamp)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });
        modelBuilder.Entity<Kashef>(entity =>
        {
            entity.ToTable("Kashef");

            entity.HasKey(e => e.ReportNumber);

            entity.Property(e => e.ReportNumber)
                .HasColumnName("رقم الكشف");

            entity.Property(e => e.ReportDate)
                .HasColumnName("تاريخ الكشف");

            entity.Property(e => e.DemandNumber)
                .HasColumnName("رقم المعاملة");

            entity.Property(e => e.DemandType)
                .HasColumnName("نوع المعاملة")
                .HasMaxLength(300);

            entity.Property(e => e.Applicant)
                .HasColumnName("المستدعي")
                .HasMaxLength(200);

            entity.Property(e => e.Owner)
                .HasColumnName("المالك")
                .HasMaxLength(200);

            entity.Property(e => e.RealEstate)
                .HasColumnName("العقار");

            entity.Property(e => e.Section)
                .HasColumnName("القسم");

            entity.Property(e => e.Block)
                .HasColumnName("البلوك")
                .HasMaxLength(50);

            entity.Property(e => e.Floor)
                .HasColumnName("الطابق");

            entity.Property(e => e.Side)
                .HasColumnName("الجهة")
                .HasMaxLength(100);

            entity.Property(e => e.Area)
                .HasColumnName("المساحة");

            entity.Property(e => e.WorkType)
                .HasColumnName("نوع الاشغال")
                .HasMaxLength(200);

            entity.Property(e => e.Neighborhood)
                .HasColumnName("الحي")
                .HasMaxLength(200);

            entity.Property(e => e.RealEstateArea)
                .HasColumnName("المنطقة العقارة")
                .HasMaxLength(200);

            entity.Property(e => e.ExtraNotes)
                .HasColumnName("ملاحظات اضافية");

            entity.Property(e => e.OccupiedOrVacant)
                .HasColumnName("مشغول او شاغر")
                .HasMaxLength(100);

            entity.Property(e => e.OccupiedBy)
                .HasColumnName("مشغول من قبل");

            entity.Property(e => e.FinishDate)
                .HasColumnName("تاريخ الانجاز");

            entity.Property(e => e.MatchesDeed)
                .HasColumnName("مطابق للافادة العقارية")
                .HasMaxLength(100);

            entity.Property(e => e.DigitalAddress)
                .HasColumnName("العنوان الرقمي")
                .HasMaxLength(200);
        });

        modelBuilder.Entity<AppSubCode>(entity =>
        {
            entity.HasKey(e => e.AppSubCodeId);

            entity.ToTable("AppSubCode");

            entity.Property(e => e.AppSubCodeId).ValueGeneratedNever();
            entity.Property(e => e.AppSubCodeAname)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("AppSubCodeAName");
            entity.Property(e => e.AppSubCodeCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.AppSubCodeDescription)
                .HasMaxLength(500)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.AppSubCodeName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDefault)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.TtimeStamp)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("Permissions");
            entity.HasKey(x => new { x.AppUserId, x.PermissionName });

            entity.Property(x => x.AppUserId).HasColumnName("AppUserID");
            entity.Property(x => x.PermissionName).HasColumnName("Permission").HasMaxLength(200);

            entity.HasOne(x => x.AppUser)
                  .WithMany()
                  .HasForeignKey(x => x.AppUserId)
                  .HasPrincipalKey(u => u.AppUserId) // AppUser.AppUserId maps to AppUserID
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.ToTable("AppUser");

            entity.Property(e => e.AppUserId).HasColumnName("AppUserID");
            entity.Property(e => e.AppUserCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.AppUserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.HashedPassword)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.IsGroupLeader)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.LastHostUsed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.LastPwdChangeDt).HasColumnType("datetime");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.LimitToBranch)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.TtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            
            // Ignore SignatureImage property as it doesn't exist in the database table
            entity.Ignore(e => e.SignatureImage);
        });

        modelBuilder.Entity<Appcode>(entity =>
        {
            entity.HasKey(e => e.AppcodeId);

            entity.ToTable("Appcode");

            entity.Property(e => e.AppcodeId).ValueGeneratedNever();
            entity.Property(e => e.AppCodeCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.AppcodeAname)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("AppcodeAName");
            entity.Property(e => e.AppcodeDescription)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.AppcodeName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.HasChild)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.IsDefault)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.TtimeStampa)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("TTimeStampa");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.Property(e => e.ArticleId).ValueGeneratedNever();
            entity.Property(e => e.ArticleText).HasMaxLength(2000);
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.TtimeStamp)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.ToTable("Branch");

            entity.Property(e => e.BranchAddress)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.BranchDescrition)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.BranchFax)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.BranchName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.BranchTel)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.IsAmainBranch)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN")
                .HasColumnName("IsAMainBranch");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.TtimeStamp)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });

        modelBuilder.Entity<BulkSm>(entity =>
        {
            entity.HasKey(e => e.Smsid).HasName("PK__BulkSMS__51300E55");

            entity.ToTable("BulkSMS");

            entity.Property(e => e.Smsid)
                .ValueGeneratedNever()
                .HasColumnName("SMSId");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.Phones)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.Sms)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("SMS");
            entity.Property(e => e.TtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CaptionChange>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("PK_CaptionChange_1");

            entity.ToTable("CaptionChange");

            entity.Property(e => e.RowId).ValueGeneratedNever();
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.Note1)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Note2)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Note3)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ref1)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ref2)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ref3)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ref4)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ref5)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ref6)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ref7)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ref8)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ref9)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });

        modelBuilder.Entity<Demand>(entity =>
        {
            entity.Property(e => e.DemandId).ValueGeneratedNever();
            entity.Property(e => e.Amount)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ApplicantDesc)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ApplicantJob)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Block)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CallerDesc)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.CallerIs)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CallerJob)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CompletionDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DemandCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DetectionNum)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FinalStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Floor)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Hay)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.MsalePrice)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MSalePrice");
            entity.Property(e => e.Near)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Signature)
     .HasColumnType("nvarchar(max)");

            entity.Property(e => e.Note2)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Note3)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Part)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PdfSourcePath)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.RealEstate)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Realestatearea)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ReceivedDate).HasColumnType("datetime");
            entity.Property(e => e.Ref1)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Ref2)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Ref3)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Ref4)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Ref5)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Ref6)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Ref7)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Ref8)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Ref9)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.ResultExp)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.Stype)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TrxnDate).HasColumnType("datetime");
            entity.Property(e => e.TrxnSubject)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.TtimeStamp)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.ValueDate).HasColumnType("datetime");

            entity.HasOne(d => d.CallerNameNavigation).WithMany(p => p.Demands)
                .HasForeignKey(d => d.CallerName)
                .HasConstraintName("FK_Demands_Persons");

            entity.HasOne(d => d.Row).WithMany(p => p.Demands)
                .HasForeignKey(d => d.RowId)
                .HasConstraintName("FK_Demands_DemandsName");
        });

        modelBuilder.Entity<DemandAvailableDoc>(entity =>
        {
            entity.HasKey(e => new { e.DemandId, e.DocumentId });

            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.TtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });

        modelBuilder.Entity<DemandStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.ToTable("DemandStatus");

            entity.Property(e => e.StatusId).ValueGeneratedNever();
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.StatusDesc)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TtimeStamp)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });

        modelBuilder.Entity<DemandsField>(entity =>
        {
            entity.HasKey(e => new { e.RowId, e.FieldId });

            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.SortNum).HasColumnType("decimal(18, 3)");
            entity.Property(e => e.TtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });

        modelBuilder.Entity<DemandsName>(entity =>
        {
            entity.HasKey(e => e.RowId);

            entity.ToTable("DemandsName");

            entity.Property(e => e.RowId).ValueGeneratedNever();
            entity.Property(e => e.DemandCode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DemandName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DocFileName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.TtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });

        modelBuilder.Entity<DemandsNameDocument>(entity =>
        {
            entity.HasKey(e => new { e.RowId, e.DocumentId });

            entity.Property(e => e.IsAdd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.TtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");

            entity.HasOne(d => d.Row).WithMany(p => p.DemandsNameDocuments)
                .HasForeignKey(d => d.RowId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DemandsNameDocuments_DemandsName");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.Property(e => e.DocumentId).ValueGeneratedNever();
            entity.Property(e => e.DocumentName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsReq)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.TtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });

        modelBuilder.Entity<FelidDictionary>(entity =>
        {
            entity.HasKey(e => e.FieldId);

            entity.ToTable("FelidDictionary");

            entity.Property(e => e.FieldId).ValueGeneratedNever();
            entity.Property(e => e.FieldCaprionName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FieldCaprionTxt)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FieldFather).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.FieldName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FieldObjectName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FieldReplacement)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsRequired)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.Ord).HasColumnName("ord");
            entity.Property(e => e.TtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });

        modelBuilder.Entity<GroupMenu>(entity =>
        {
            entity.ToTable("GroupMenu");

            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.MenuName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.TtimeStamp)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });

        modelBuilder.Entity<Ihaler>(entity =>
        {
            entity.ToTable("Ihaler");

            entity.Property(e => e.IhalerId).ValueGeneratedNever();
            entity.Property(e => e.AppGroupId).HasColumnName("AppGroupID");
            entity.Property(e => e.IhalerDate).HasColumnType("datetime");
            entity.Property(e => e.IhalerDesc)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.IhalerRevissionDate).HasColumnType("datetime");
            entity.Property(e => e.IhalerSign).HasColumnType("image");
            entity.Property(e => e.IhalerStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IsLast)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.KarrarDate).HasColumnType("datetime");
            entity.Property(e => e.KarrarNum)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.ReceivedDate).HasColumnType("datetime");
            entity.Property(e => e.TtimeStamp)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");

            entity.HasOne(d => d.Demand).WithMany(p => p.Ihalers)
                .HasForeignKey(d => d.DemandId)
                .HasConstraintName("FK_Ihaler_Demands");
        });

        modelBuilder.Entity<Inbox>(entity =>
        {
            entity.HasKey(e => e.RowId);

            entity.Property(e => e.RowId).ValueGeneratedNever();
            entity.Property(e => e.AppUserInboxs)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AppUserNameFrom)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AppUserNameTo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.SourceName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Subject).HasColumnType("text");
        });

        modelBuilder.Entity<LastId>(entity =>
        {
            entity.HasKey(e => e.TableName);

            entity.ToTable("LastId");

            entity.Property(e => e.TableName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.LastId1).HasColumnName("LastID");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.TtimeStamp)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("TTimeStamp");
        });

        modelBuilder.Entity<Ministry>(entity =>
        {
            entity.ToTable("Ministry");

            entity.Property(e => e.MinistryId).ValueGeneratedNever();
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.MinistrryLogo).HasColumnType("image");
            entity.Property(e => e.MinistryAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MinistryName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MinistryPhone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MinistryWeb)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.STtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("sTTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Municipality>(entity =>
        {
            entity.ToTable("Municipality");

            entity.Property(e => e.MunicipalityId).ValueGeneratedNever();
            entity.Property(e => e.DateOfBorn).HasColumnType("datetime");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.MunicipalityCityName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MunicipalityFax)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MunicipalityLogo).HasColumnType("image");
            entity.Property(e => e.MunicipalityMainAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MunicipalityName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MunicipalityTel)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MunicipalityWeb)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ref1)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Ref2)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TtimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId);

            entity.Property(e => e.PersonId)
                  .ValueGeneratedOnAdd()
                  .HasDefaultValueSql("NEXT VALUE FOR dbo.Seq_PersonId");
            entity.Property(e => e.BregNum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BRegNum");
            entity.Property(e => e.CortAndY)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.Notes)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.PersonCardNum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.PersonDateOfIssue).HasColumnType("datetime");
            entity.Property(e => e.PersonDoB).HasColumnType("datetime");
            entity.Property(e => e.PersonFname)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.PersonGender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.PersonImage).HasColumnType("image");
            entity.Property(e => e.PersonLname)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("PersonLName");
            entity.Property(e => e.PersonMname)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.PersonMoName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.PersonRegNum)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.PersonRegPlace)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.Ref1)
                .HasMaxLength(150)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.Ref2)
                .HasMaxLength(150)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.Ref3)
                .HasMaxLength(150)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.Reference)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.TaxNum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
            entity.Property(e => e.TtimeStamp)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasColumnName("TTimeStamp");
            entity.Property(e => e.Tvanum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TVANum");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(25)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });

        modelBuilder.Entity<Referencess>(entity =>
        {
            entity.HasKey(e => e.RowId);

            entity.ToTable("Referencess");

            entity.Property(e => e.RowId).ValueGeneratedNever();
            entity.Property(e => e.DescR)
                .HasMaxLength(300)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN");
        });
        modelBuilder.Entity<Ehsaa>(entity =>
        {
            entity.ToTable("Ehsaa");

            entity.HasKey(e => e.EhsaaId);

            entity.Property(e => e.EhsaaId).HasColumnName("EhsaaId");

            entity.Property(e => e.TaxPayerFullName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("اسم المكلف الثلاثي");

            entity.Property(e => e.RegisterNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("رقم السجل");

            entity.Property(e => e.MotherName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("اسم الام وشهرتها");

            entity.Property(e => e.Nationality)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الجنسية");

            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("رقم الهاتف");

            entity.Property(e => e.PayerRelationType)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("صفة القائم على التكليف");

            entity.Property(e => e.CommercialRegister)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("السجل التجاري");

            entity.Property(e => e.RentalOccupancyStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("صفة اشغال المأجور");

            entity.Property(e => e.ContractDetails)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("تفاصيل العلاقة التعاقدية");

            entity.Property(e => e.HasLeaseContract)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("هل يوجد عقد ايجار");

            entity.Property(e => e.OwnerName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("اسم المالك");

            entity.Property(e => e.OwnerRegisterNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("رقم السجل للمالك");

            entity.Property(e => e.OwnerMotherName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("اسم ام المالك وشهرتها");

            entity.Property(e => e.OwnerNationality)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("جنسية المالك");

            entity.Property(e => e.OwnerPhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("رقم هاتف المالك");

            entity.Property(e => e.PropertyType)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("نوع المأجور");

            entity.Property(e => e.PropertyAddress)
                .HasMaxLength(300)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("عنوان المأجور");

            entity.Property(e => e.Area)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("المساحة");

            entity.Property(e => e.RoofType)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("نوع السقف");

            entity.Property(e => e.StructureType)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الهيكل الانشائي");

            entity.Property(e => e.PropertySide)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("جهة العقار");

            entity.Property(e => e.FloorsCount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("عدد الطوابق");

            entity.Property(e => e.ApartmentNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("رقم الشقة");

            entity.Property(e => e.FloorNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الطابق");

            entity.Property(e => e.MoveInDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("تاريخ الانتقال");

            entity.Property(e => e.TenantName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("اسم لشخص ساكن المأجور");

            entity.Property(e => e.TenantMotherAndDob)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الام والموليد");

            entity.Property(e => e.TenantPhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("رقم الهاتف للشخص الساكن");

            entity.Property(e => e.TenantNationality)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الجنسية الساكن");

            entity.Property(e => e.ResidentsCount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("عدد المقيمين بالبيت");

            entity.Property(e => e.TenantsCount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("عدد السكارة");

            entity.Property(e => e.TenantRelationType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الساكن قريب او اجنبي");

            entity.Property(e => e.IsEmployee)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الموظف");

            entity.Property(e => e.RelativeFromFamily)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("اذا اقارب اي فرد من العائلة");

            entity.Property(e => e.RelativeName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("اسمه");

            entity.Property(e => e.RelationDetails)
                .HasMaxLength(300)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("حيثية العلاقة");

            entity.Property(e => e.JobType)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("نوع الوظيفة");

            entity.Property(e => e.Employer)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الجهة التي يعمل به");

            entity.Property(e => e.PersonMonthlyIncome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("مدخول الفرد من العمل/ الشهر");

            entity.Property(e => e.FamilyNonWorkingCount)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("مدخول العائلة غير الرابح ( كم شخص عاطل عن العمل )");

            entity.Property(e => e.HasExtraWorkToReduceTaxes)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("هل يوجد عمل اضافي لانقاص كل ما يمكن من ضرائب البلدة");

            entity.Property(e => e.ExtraWorkYesNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("نعم - لا");

            entity.Property(e => e.ExtraWorkActiveStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("ممارس / غير ممارس");

            entity.Property(e => e.ExtraWorkType)
                .HasMaxLength(300)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("ماهو نوع العمل");

            entity.Property(e => e.ExtraWorkMonthlyIncome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الدخل الشهري من العمل اذا ممارس");

            entity.Property(e => e.HasSocialAid)
                .HasMaxLength(300)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("المستفيد من صناديق الضمان والمساعدات الاجتماعية وغيرها من مؤسسات");

            entity.Property(e => e.SocialAidSources)
                .HasMaxLength(300)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("اذا نعم ما هي الجهات ");

            entity.Property(e => e.WaterLocation)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("اماكن توجد المياه");

            entity.Property(e => e.WaterSource)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("مصدر المياه");

            entity.Property(e => e.WorkInsideOrOutsideTown)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("عمل الفرد داخل البلدة / خارجها");

            entity.Property(e => e.DisabilityType)
                .HasMaxLength(300)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("نوع الاعاقة الذهنية او الجسدية");

            entity.Property(e => e.EducationLevel)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("حاصل الفرد العلمي");

            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الحالة الاجتماعية");

            entity.Property(e => e.HasInvestmentFunding)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("هل تستفيد العائلة من التمويل حاليا لاي مشروع استثماري");

            entity.Property(e => e.InvestmentFundingSource)
                .HasMaxLength(300)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("اذا نعم اي جهة ممول الشركات او اقراض");

            entity.Property(e => e.WantsProductionProject)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("هل يرغب في مشروع انتاجي");

            entity.Property(e => e.ProjectWishType)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("رغبة خاصية ام عائلية");

            entity.Property(e => e.ProjectType)
                .HasMaxLength(300)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("نوع المشروع");

            entity.Property(e => e.RequiredCapital)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("راس المال المطلوب");

            entity.Property(e => e.HasPrivateGenerator)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("هل يوجد مولد خاص");

            entity.Property(e => e.HasElectricitySubscription)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("هل يوجد اشتراك كهرباء");

            entity.Property(e => e.Ampere)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الامبير");

            entity.Property(e => e.DistributionAuthority)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("جهة التوزيع");

            entity.Property(e => e.HasTent)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("هل يوجد خيمة");

            entity.Property(e => e.TentsCount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("عدد الخيم");

            entity.Property(e => e.TentsArea)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("مساحة الخيم");

            entity.Property(e => e.HasArtesianWell)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("بئر ارتوازي");

            entity.Property(e => e.HasSewer)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("هل يوجد مجاري");

            entity.Property(e => e.Sanitation)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الاكلير الصحي");

            entity.Property(e => e.HasParking)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("هل يوجد موقف");

            entity.Property(e => e.ParkingSlotsCount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("عدد المواقف");

            entity.Property(e => e.HasDisabledPerson)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("هل يوجد اعاقة");

            entity.Property(e => e.DisabledPersonName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("الشخص المعاق");

            entity.Property(e => e.DisabledPersonCardNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("رقم البطاقة");

            entity.Property(e => e.ExtraNote)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .UseCollation("Arabic_BIN")
                .HasColumnName("ملاحظة اضافية");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
