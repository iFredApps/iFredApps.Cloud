namespace iFredApps.Cloud.Core.Models
{
   public class User : UserData
   {
      public Guid UserId { get; set; }
      public string PasswordHash { get; set; }
      public bool IsAdmin { get; set; }
      public DateTime CreatedAt { get; set; }
      public DateTime UpdatedAt { get; set; }
      public List<License> Licenses { get; set; }
   }

   public class UserData
   {
      public string Name { get; set; }
      public string Username { get; set; }
      public string Email { get; set; }
      public DateTime? BirthdayDate { get; set; }
      public string Cellphone { get; set; }
      public string Telephone { get; set; }
      public string Country { get; set; }
      public string City { get; set; }
   }
}
