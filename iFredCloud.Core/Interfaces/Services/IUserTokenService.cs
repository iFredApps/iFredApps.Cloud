using iFredCloud.Core.Models;

namespace iFredCloud.Core.Interfaces.Services
{
    public interface IUserTokenService
    {
        Task<string> GenerateToken(User user);
        Task<bool> IsTokenValid(string token);
    }
}
