using iFredCloud.Core.Interfaces.Services;
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
      private readonly IUserTokenService _userTokenService;
      private readonly string _key;
      private readonly string _issuer;
      private readonly string _audience;

      public AuthController(IUserService userService, IUserTokenService userTokenService, IConfiguration configuration)
      {
         _userService = userService;
         _userTokenService = userTokenService;

         _key = configuration["Jwt:Key"];
         _issuer = configuration["Jwt:Issuer"];
         _audience = configuration["Jwt:Audience"];
      }

      [HttpPost("login")]
      public async Task<IActionResult> Login([FromBody] LoginModel model)
      {
         var validUser = await _userService.ValidateUser(model.UserSearchKey, model.Password);
         if (validUser == null)
         {
            return Unauthorized();
         }

         var user = await _userService.GetUserByUsername(model.UserSearchKey);
         if (user == null)
            user = await _userService.GetUserByEmail(model.UserSearchKey);

         var token = await _userTokenService.GenerateToken(user);

         return Ok(new { Token = token });
      }
   }

   public class LoginModel
   {
      public string UserSearchKey { get; set; }
      public string Password { get; set; }
   }
}
