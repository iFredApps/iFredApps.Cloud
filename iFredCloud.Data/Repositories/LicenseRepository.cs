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
   public class LicenseRepository : ILicenseRepository
   {
      private readonly AppDbContext _context;

      public LicenseRepository(AppDbContext context)
      {
         _context = context;
      }

      public async Task<License> GetLicenseByUserAndServiceAsync(Guid userId, string serviceType)
      {
         return await _context.Licenses
                              .FirstOrDefaultAsync(l => l.UserId == userId && l.ServiceName == serviceType);
      }

      public async Task AddLicenseAsync(License license)
      {
         await _context.Licenses.AddAsync(license);
         await _context.SaveChangesAsync();
      }
   }
}
