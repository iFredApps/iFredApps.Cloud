using iFredCloud.Core.Models;

namespace iFredCloud.Core.Interfaces.Services
{
   public interface IUserService
   {
      Task<User> AuthenticateAsync(string user, string password);
      Task RegisterUserAsync(UserData userData, string password);
   }
}
