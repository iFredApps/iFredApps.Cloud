using iFredCloud.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFredCloud.Core.Interfaces
{
   public interface IUserRepository
   {
      Task<IEnumerable<User>> GetAllUsers();
      Task<User> GetUserById(int id);
      Task<User> GetUserByUsername(string username);
      Task<User> GetUserByEmail(string email);
      Task AddUser(User user);
      Task UpdateUser(User user);
      Task DeleteUser(int id);
   }
}
