namespace iFredApps.Cloud.Core.Models
{
   public class License : LicenseData
   {
      public Guid LicenseId { get; set; }
   }

   public class LicenseData
   {
      public Guid UserId { get; set; }
      public string LicenseType { get; set; }
      public string ServiceName { get; set; }
      public DateTime? ExpirationDate { get; set; }
      public int? MaxQuota { get; set; }
      public int UsageCount { get; set; }
      public DateTime CreatedAt { get; set; }
      public DateTime UpdatedAt { get; set; }
   }

   public struct sttLicenseType
   {
      public static string LifeTime = "LIFETIME";
      public static string UsageLimit = "USAGE_LIMIT";
      public static string Recurrent = "RECURRENT";
   }
}
