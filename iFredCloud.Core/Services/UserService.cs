using iFredApps.Lib.Base;
using iFredCloud.Core.Interfaces.Repository;
using iFredCloud.Core.Interfaces.Services;
using iFredCloud.Core.Models;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace iFredCloud.Core.Services
{
   public class UserService : IUserService
   {
      private readonly IUserRepository _userRepository;

      public UserService(IUserRepository userRepository)
      {
         _userRepository = userRepository;
      }

      public async Task<User> AuthenticateAsync(string user, string password)
      {
         var userData = await _userRepository.GetUserAsync(user);
         if (userData == null || userData.PasswordHash != HashPassword(password))
            return null;

         return userData;
      }

      public async Task RegisterUserAsync(UserData userData, string password)
      {
         ValidateUserData(userData);

         User user = new User
         {
            UserId = Guid.NewGuid(),
            PasswordHash = HashPassword(password),
            Name = userData.Name,
            Username = userData.Username,
            Email = userData.Email,
            BirthdayDate = userData.BirthdayDate,
            Cellphone = userData.Cellphone,
            Telephone = userData.Telephone,
            Country = userData.Country,
            City = userData.City,
         };

         await _userRepository.AddUserAsync(user);
      }

      private void ValidateUserData(UserData userData)
      {
         if (!Validations.IsValidEmail(userData.Email))
            throw new Exception("O email fornecido é inválido.");
      }

      private string HashPassword(string password)
      {
         using var sha256 = SHA256.Create();
         var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
         return Convert.ToBase64String(bytes);
      }
   }
}
