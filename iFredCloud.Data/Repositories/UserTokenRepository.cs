using iFredCloud.Core.Interfaces.Repository;
using iFredCloud.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFredCloud.Data.Repositories
{
   public class UserTokenRepository : IUserTokenRepository
   {
      private readonly AppDbContext _context;

      public UserTokenRepository(AppDbContext context)
      {
         _context = context;
      }

      public async Task AddToken(UserToken token)
      {
         _context.UsersTokens.Add(token);
         await _context.SaveChangesAsync();
      }

      public async Task<bool> IsTokenValid(string token)
      {
         var tokenEntity = await _context.UsersTokens.FirstOrDefaultAsync(t => t.jwt_token == token);
         return tokenEntity != null && tokenEntity.expiration > DateTime.UtcNow;
      }
   }
}
