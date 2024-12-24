using iFredCloud.Core.Interfaces.Repository;
using iFredCloud.Core.Interfaces.Services;
using iFredCloud.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFredCloud.Core.Services
{
   public class LicenseService : ILicenseService
   {
      private readonly ILicenseRepository _licenseRepository;

      public LicenseService(ILicenseRepository licenseRepository)
      {
         _licenseRepository = licenseRepository;
      }

      public async Task<License> GetLicenseAsync(Guid userId, string serviceType)
      {
         return await _licenseRepository.GetLicenseByUserAndServiceAsync(userId, serviceType);
      }

      public async Task<bool> ValidateLicenseAsync(Guid userId, string serviceType)
      {
         var license = await _licenseRepository.GetLicenseByUserAndServiceAsync(userId, serviceType);

         if (license == null) return false;

         // Verificar data de expiração e quota máxima
         if (license.ExpirationDate.HasValue && license.ExpirationDate.Value < DateTime.UtcNow) return false;
         if (license.MaxQuota.HasValue && license.UsageCount >= license.MaxQuota.Value) return false;

         return true;
      }

      public async Task AddLicenseAsync(LicenseData licenseData)
      {
         License license = new License
         {
            LicenseId = Guid.NewGuid(),
            UserId = licenseData.UserId,
            ServiceName = licenseData.ServiceName,
            LicenseType = licenseData.LicenseType,
            ExpirationDate = licenseData.ExpirationDate,
            MaxQuota = licenseData.MaxQuota,
            UsageCount = licenseData.UsageCount,
         };

         await _licenseRepository.AddLicenseAsync(license);
      }
   }
}
