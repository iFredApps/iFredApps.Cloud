using iFredCloud.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFredCloud.Core.Interfaces.Repository
{
   public interface IUserRepository
   {
      Task<User> GetUserAsync(string user);
      Task<User> GetUserByIdAsync(Guid userId);
      Task AddUserAsync(User user);
   }
}
