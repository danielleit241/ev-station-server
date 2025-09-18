using EV_Station.Domain.Models;

namespace EV_Station.Application.Common.Abstractions.IServices
{
    public interface ITokenService
    {
        public string GenerateAccessToken(User user);
        public string GenerateRefreshToken();
        public string GenerateAndMapRefreshToken(User user);
    }
}
