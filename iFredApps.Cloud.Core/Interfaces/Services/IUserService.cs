using iFredApps.Cloud.Core.Models;

namespace iFredApps.Cloud.Core.Interfaces.Services
{
   public interface IUserService
   {
      Task<User> GetUserByIdAsync(Guid userId);
      Task<User> AuthenticateAsync(string user, string password);
      Task RegisterUserAsync(UserData userData, string password);
   }
}
