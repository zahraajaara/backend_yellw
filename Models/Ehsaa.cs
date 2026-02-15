using System;

namespace YellowKalam.Api.Models
{
    public partial class Ehsaa
    {
        public int EhsaaId { get; set; }

        public string? TaxPayerFullName { get; set; }              // اسم المكلف الثلاثي
        public string? RegisterNumber { get; set; }                // رقم السجل
        public string? MotherName { get; set; }                    // اسم الام وشهرتها
        public string? Nationality { get; set; }                   // الجنسية
        public string? PhoneNumber { get; set; }                   // رقم الهاتف
        public string? PayerRelationType { get; set; }             // صفة القائم على التكليف
        public string? CommercialRegister { get; set; }            // السجل التجاري
        public string? RentalOccupancyStatus { get; set; }         // صفة اشغال المأجور
        public string? ContractDetails { get; set; }               // تفاصيل العلاقة التعاقدية
        public string? HasLeaseContract { get; set; }              // هل يوجد عقد ايجار
        public string? OwnerName { get; set; }                     // اسم المالك
        public string? OwnerRegisterNumber { get; set; }           // رقم السجل للمالك
        public string? OwnerMotherName { get; set; }               // اسم ام المالك وشهرتها
        public string? OwnerNationality { get; set; }              // جنسية المالك
        public string? OwnerPhoneNumber { get; set; }              // رقم هاتف المالك
        public string? PropertyType { get; set; }                  // نوع المأجور
        public string? PropertyAddress { get; set; }               // عنوان المأجور
        public string? Area { get; set; }                          // المساحة
        public string? RoofType { get; set; }                      // نوع السقف
        public string? StructureType { get; set; }                 // الهيكل الانشائي
        public string? PropertySide { get; set; }                  // جهة العقار
        public string? FloorsCount { get; set; }                   // عدد الطوابق
        public string? ApartmentNumber { get; set; }               // رقم الشقة
        public string? FloorNumber { get; set; }                   // الطابق
        public string? MoveInDate { get; set; }                    // تاريخ الانتقال (string for flexibility)
        public string? TenantName { get; set; }                    // اسم لشخص ساكن المأجور
        public string? TenantMotherAndDob { get; set; }            // الام والموليد
        public string? TenantPhoneNumber { get; set; }             // رقم الهاتف للشخص الساكن
        public string? TenantNationality { get; set; }             // الجنسية الساكن
        public string? ResidentsCount { get; set; }                // عدد المقيمين بالبيت
        public string? TenantsCount { get; set; }                  // عدد السكارة
        public string? TenantRelationType { get; set; }            // الساكن قريب او اجنبي
        public string? IsEmployee { get; set; }                    // الموظف
        public string? RelativeFromFamily { get; set; }            // اذا اقارب اي فرد من العائلة
        public string? RelativeName { get; set; }                  // اسمه
        public string? RelationDetails { get; set; }               // حيثية العلاقة
        public string? JobType { get; set; }                       // نوع الوظيفة
        public string? Employer { get; set; }                      // الجهة التي يعمل به
        public string? PersonMonthlyIncome { get; set; }           // مدخول الفرد من العمل/ الشهر
        public string? FamilyNonWorkingCount { get; set; }         // مدخول العائلة غير الرابح ...

        public string? HasExtraWorkToReduceTaxes { get; set; }     // هل يوجد عمل اضافي ...
        public string? ExtraWorkYesNo { get; set; }                // نعم - لا
        public string? ExtraWorkActiveStatus { get; set; }         // ممارس / غير ممارس
        public string? ExtraWorkType { get; set; }                 // ماهو نوع العمل
        public string? ExtraWorkMonthlyIncome { get; set; }        // الدخل الشهري من العمل اذا ممارس
        public string? HasSocialAid { get; set; }                  // المستفيد من صناديق الضمان...
        public string? SocialAidSources { get; set; }              // اذا نعم ما هي الجهات
        public string? WaterLocation { get; set; }                 // اماكن توجد المياه
        public string? WaterSource { get; set; }                   // مصدر المياه
        public string? WorkInsideOrOutsideTown { get; set; }       // عمل الفرد داخل البلدة / خارجها
        public string? DisabilityType { get; set; }                // نوع الاعاقة الذهنية او الجسدية
        public string? EducationLevel { get; set; }                // حاصل الفرد العلمي
        public string? MaritalStatus { get; set; }                 // الحالة الاجتماعية
        public string? HasInvestmentFunding { get; set; }          // هل تستفيد العائلة من التمويل...
        public string? InvestmentFundingSource { get; set; }       // اذا نعم اي جهة ممول...
        public string? WantsProductionProject { get; set; }        // هل يرغب في مشروع انتاجي
        public string? ProjectWishType { get; set; }               // رغبة خاصية ام عائلية
        public string? ProjectType { get; set; }                   // نوع المشروع
        public string? RequiredCapital { get; set; }               // راس المال المطلوب
        public string? HasPrivateGenerator { get; set; }           // هل يوجد مولد خاص

        public string? HasElectricitySubscription { get; set; }    // هل يوجد اشتراك كهرباء
        public string? Ampere { get; set; }                        // الامبير
        public string? DistributionAuthority { get; set; }         // جهة التوزيع
        public string? HasTent { get; set; }                       // هل يوجد خيمة
        public string? TentsCount { get; set; }                    // عدد الخيم
        public string? TentsArea { get; set; }                     // مساحة الخيم
        public string? HasArtesianWell { get; set; }               // بئر ارتوازي
        public string? HasSewer { get; set; }                      // هل يوجد مجاري
        public string? Sanitation { get; set; }                    // الاكلير الصحي
        public string? HasParking { get; set; }                    // هل يوجد موقف
        public string? ParkingSlotsCount { get; set; }             // عدد المواقف
        public string? HasDisabledPerson { get; set; }             // هل يوجد اعاقة
        public string? DisabledPersonName { get; set; }            // الشخص المعاق
        public string? DisabledPersonCardNumber { get; set; }      // رقم البطاقة
        public string? ExtraNote { get; set; }                     // ملاحظة اضافية
    }
}
