using EV_Station.Domain.Models;

namespace EV_Station.Application.Common.Abstractions.IServices
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}
