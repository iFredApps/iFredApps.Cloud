using iFredApps.Cloud.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFredApps.Cloud.Core.Interfaces.Services
{
   public interface ILicenseService
   {
      Task<License> GetLicenseAsync(Guid userId, string serviceType);
      Task<bool> ValidateLicenseAsync(Guid userId, string serviceType);
      Task AddLicenseAsync(LicenseData licenseData);
   }
}
