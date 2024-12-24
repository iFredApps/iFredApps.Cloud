using iFredCloud.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iFredCloud.Core.Interfaces.Repository
{
   public interface ILicenseRepository
   {
      Task<License> GetLicenseByUserAndServiceAsync(Guid userId, string serviceType);
      Task AddLicenseAsync(License license);
   }
}
