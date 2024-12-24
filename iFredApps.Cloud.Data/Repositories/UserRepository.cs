using iFredApps.Cloud.Core.Interfaces.Repository;
using iFredApps.Cloud.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFredApps.Cloud.Data.Repositories
{
   public class UserRepository : IUserRepository
   {
      private readonly AppDbContext _context;

      public UserRepository(AppDbContext context)
      {
         _context = context;
      }

      public async Task<User> GetUserAsync(string user)
      {
         return await _context.Users.FirstOrDefaultAsync(u => u.Email == user || u.Username == user);
      }

      public async Task<User> GetUserByIdAsync(Guid userId)
      {
         return await _context.Users.FindAsync(userId);
      }

      public async Task AddUserAsync(User user)
      {
         await _context.Users.AddAsync(user);
         await _context.SaveChangesAsync();
      }
   }
}
