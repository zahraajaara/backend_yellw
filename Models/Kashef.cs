using System;

namespace YellowKalam.Api.Models
{
    public class Kashef
    {
        public long ReportNumber { get; set; }        // رقم الكشف
        public DateTime? ReportDate { get; set; }     // تاريخ الكشف
        public int? DemandNumber { get; set; }        // رقم المعاملة
        public string? DemandType { get; set; }       // نوع المعاملة
        public string? Applicant { get; set; }        // المستدعي
        public string? Owner { get; set; }            // المالك
        public int? RealEstate { get; set; }          // العقار
        public int? Section { get; set; }             // القسم
        public string? Block { get; set; }            // البلوك
        public int? Floor { get; set; }               // الطابق
        public string? Side { get; set; }             // الجهة
        public int? Area { get; set; }                // المساحة
        public string? WorkType { get; set; }         // نوع الاشغال
        public string? Neighborhood { get; set; }     // الحي
        public string? RealEstateArea { get; set; }   // المنطقة العقارة
        public string? ExtraNotes { get; set; }       // ملاحظات اضافية
        public string? OccupiedOrVacant { get; set; } // مشغول او شاغر
        public int? OccupiedBy { get; set; }          // مشغول من قبل
        public DateTime? FinishDate { get; set; }     // تاريخ الانجاز
        public string? MatchesDeed { get; set; }      // مطابق للافادة العقارية
        public string? DigitalAddress { get; set; }   // العنوان الرقمي
    }
}
