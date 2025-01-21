using iFredApps.Cloud.Core.Models;
using System.Security.Claims;

namespace iFredApps.Cloud.Api.Models
{
   public static class UserHelper
   {
      public static Guid GetUserIdFromClaims(ClaimsPrincipal user)
      {
         var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
         if (userIdClaim == null)
         {
            throw new InvalidOperationException("User ID claim is missing");
         }

         return Guid.Parse(userIdClaim.Value);
      }
   }
}
