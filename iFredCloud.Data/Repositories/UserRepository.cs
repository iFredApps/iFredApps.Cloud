using iFredCloud.Core.Interfaces;
using iFredCloud.Core.Models;
using iFredCloud.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFredCloud.Data.Repositories
{
   public class UserRepository : IUserRepository
   {
      private readonly AppDbContext _context;

      public UserRepository(AppDbContext context)
      {
         _context = context;
      }

      public async Task<IEnumerable<User>> GetAllUsers()
      {
         return await _context.Users.ToListAsync();
      }

      public async Task<User> GetUserById(int id)
      {
         return await _context.Users.FindAsync(id);
      }

      public async Task<User> GetUserByUsername(string username)
      {
         return await _context.Users.FirstOrDefaultAsync(u => u.username == username);
      }

      public async Task<User> GetUserByEmail(string email)
      {
         return await _context.Users.FirstOrDefaultAsync(u => u.email == email);
      }

      public async Task AddUser(User user)
      {
         await _context.Users.AddAsync(user);
         await _context.SaveChangesAsync();
      }

      public async Task UpdateUser(User user)
      {
         _context.Users.Update(user);
         await _context.SaveChangesAsync();
      }

      public async Task DeleteUser(int id)
      {
         var user = await _context.Users.FindAsync(id);
         if (user != null)
         {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
         }
      }
   }
}
