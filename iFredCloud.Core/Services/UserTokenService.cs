using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using iFredCloud.Core.Models;
using iFredCloud.Core.Interfaces.Services;
using iFredCloud.Core.Interfaces.Repository;

namespace iFredCloud.Core.Services
{
   public class UserTokenService : IUserTokenService
   {
      private readonly IConfiguration _configuration;
      private readonly IUserTokenRepository _tokenRepository;

      public UserTokenService(IConfiguration configuration, IUserTokenRepository tokenRepository)
      {
         _configuration = configuration;
         _tokenRepository = tokenRepository;
      }

      public async Task<string> GenerateToken(User user)
      {
         var tokenHandler = new JwtSecurityTokenHandler();
         var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
         var tokenDescriptor = new SecurityTokenDescriptor
         {
            Subject = new ClaimsIdentity(new Claim[] 
            {
               new Claim(ClaimTypes.Name, user.username)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
         };
         var token = tokenHandler.CreateToken(tokenDescriptor);
         var tokenString = tokenHandler.WriteToken(token);

         var tokenEntity = new UserToken
         {
            user_id = user.id.ToString(),
            jwt_token = tokenString,
            expiration = tokenDescriptor.Expires.Value
         };

         await _tokenRepository.AddToken(tokenEntity);

         return tokenString;
      }

      public Task<bool> IsTokenValid(string token) => _tokenRepository.IsTokenValid(token);
   }
}
