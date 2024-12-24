using iFredApps.Cloud.Core.Interfaces.Services;
using iFredApps.Cloud.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;

namespace iFredApps.Cloud.Api.Controllers
{
   [ApiController]
   [Route("api/license")]
   [Authorize]
   public class LicenseController : ControllerBase
   {
      private readonly ILicenseService _licenseService;

      public LicenseController(ILicenseService licenseService)
      {
         _licenseService = licenseService;
      }

      [HttpGet("{serviceType}")]
      public async Task<IActionResult> GetLicense(string serviceType)
      {
         var userId = GetUserIdFromToken();
         var license = await _licenseService.GetLicenseAsync(userId, serviceType);

         if (license == null)
            return NotFound("No license found for this service");

         return Ok(license);
      }

      [HttpPost("validate/{serviceType}")]
      public async Task<IActionResult> ValidateLicense(string serviceType)
      {
         var userId = GetUserIdFromToken();
         var isValid = await _licenseService.ValidateLicenseAsync(userId, serviceType);

         return Ok(new { IsValid = isValid });
      }

      [HttpPost]
      [Authorize(Roles = "Admin")] // Apenas admins podem criar licenças
      public async Task<IActionResult> AddLicense([FromBody] LicenseData request)
      {
         await _licenseService.AddLicenseAsync(request);
         return Ok("License created successfully");
      }

      private Guid GetUserIdFromToken()
      {
         return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
      }
   }
}
