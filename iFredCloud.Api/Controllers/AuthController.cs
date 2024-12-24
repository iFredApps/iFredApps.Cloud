using iFredCloud.Core.Interfaces.Services;
using iFredCloud.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iFredCloud.Api.Controllers
{
   [ApiController]
   [Route("api/auth")]
   public class AuthController : ControllerBase
   {
      private readonly IUserService _userService;
      private readonly IConfiguration _configuration;

      public AuthController(IUserService userService, IConfiguration configuration)
      {
         _userService = userService;
         _configuration = configuration;
      }

      [HttpPost("login")]
      public async Task<IActionResult> Login([FromBody] LoginRequest request)
      {
         var user = await _userService.AuthenticateAsync(request.User, request.Password);

         if (user == null)
            return Unauthorized("Invalid email or password");

         var token = GenerateJwtToken(user);
         return Ok(new { Token = token });
      }

      [HttpPost("register")]
      public async Task<IActionResult> Register([FromBody] RegisterRequest request)
      {
         await _userService.RegisterUserAsync(request, request.Password);
         return Ok("User registered successfully");
      }

      private string GenerateJwtToken(User user)
      {
         List<Claim> claims = new List<Claim>
         {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
         };

         if(user.IsAdmin)
         {
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
         }

         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

         var token = new JwtSecurityToken(
             issuer: _configuration["Jwt:Issuer"],
             audience: _configuration["Jwt:Audience"],
             claims: claims,
             expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpiryMinutes"])),
             signingCredentials: creds
         );

         return new JwtSecurityTokenHandler().WriteToken(token);
      }
   }

   public class LoginRequest
   {
      public string User { get; set; }
      public string Password { get; set; }
   }

   public class RegisterRequest : UserData
   {
      public string Password { get; set; }
   }
}
