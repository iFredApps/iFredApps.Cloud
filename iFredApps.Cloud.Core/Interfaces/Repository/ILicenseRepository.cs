using iFredApps.Cloud.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFredApps.Cloud.Core.Interfaces.Repository
{
   public interface ILicenseRepository
   {
      Task<List<License>> GetLicensesAsync(Guid userId);
      Task<License> GetLicenseByUserAndServiceAsync(Guid userId, string serviceType);
      Task AddLicenseAsync(License license);
   }
}
