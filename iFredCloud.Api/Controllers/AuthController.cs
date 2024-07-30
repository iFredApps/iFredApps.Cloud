using iFredCloud.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iFredCloud.Api.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AuthController : ControllerBase
   {
      private readonly IUserService _userService;
      private readonly string _key;
      private readonly string _issuer;
      private readonly string _audience;

      public AuthController(IUserService userService, IConfiguration configuration)
      {
         _userService = userService;
         _key = configuration["Jwt:Key"];
         _issuer = configuration["Jwt:Issuer"];
         _audience = configuration["Jwt:Audience"];
      }

      [HttpPost("login")]
      public async Task<IActionResult> Login([FromBody] LoginModel model)
      {
         var user = await _userService.ValidateUser(model.UserSearchKey, model.Password);
         if (!user)
         {
            return Unauthorized();
         }

         var tokenHandler = new JwtSecurityTokenHandler();
         var key = Encoding.ASCII.GetBytes(_key);
         var tokenDescriptor = new SecurityTokenDescriptor
         {
            Subject = new ClaimsIdentity(new Claim[] 
            {
               new Claim(ClaimTypes.Name, model.UserSearchKey)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };
         var token = tokenHandler.CreateToken(tokenDescriptor);
         var tokenString = tokenHandler.WriteToken(token);

         return Ok(new { Token = tokenString });
      }
   }

   public class LoginModel
   {
      public string UserSearchKey { get; set; }
      public string Password { get; set; }
   }
}
