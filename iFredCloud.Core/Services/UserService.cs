using iFredCloud.Core.Interfaces.Repository;
using iFredCloud.Core.Interfaces.Services;
using iFredCloud.Core.Models;

namespace iFredCloud.Core.Services
{
    public class UserService : IUserService
   {
      private readonly IUserRepository _userRepository;

      public UserService(IUserRepository userRepository)
      {
         _userRepository = userRepository;
      }

      public async Task<IEnumerable<User>> GetAllUsers()
      {
         return await _userRepository.GetAllUsers();
      }

      public async Task<User> GetUserById(int id)
      {
         return await _userRepository.GetUserById(id);
      }

      public async Task<User> GetUserByUsername(string username)
      {
         return await _userRepository.GetUserByUsername(username);
      }

      public async Task<User> GetUserByEmail(string email)
      {
         return await _userRepository.GetUserByEmail(email);
      }

      public async Task AddUser(User user)
      {
         user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
         await _userRepository.AddUser(user);
      }

      public async Task UpdateUser(User user)
      {
         if (!string.IsNullOrEmpty(user.password))
         {
            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
         }
         await _userRepository.UpdateUser(user);
      }

      public async Task DeleteUser(int id)
      {
         await _userRepository.DeleteUser(id);
      }

      public async Task<bool> ValidateUser(string userSearchKey, string plainPassword)
      {
         var user = await _userRepository.GetUserByUsername(userSearchKey);
         if (user == null)
            user = await _userRepository.GetUserByEmail(userSearchKey);

         if (user == null)
         {
            return false;
         }

         // Verify the provided password against the hashed password
         return BCrypt.Net.BCrypt.Verify(plainPassword, user.password);
      }
   }
}
