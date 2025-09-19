using EV_Station.Application.Common.Abstractions.IServices;
using Microsoft.Extensions.Configuration;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace EV_Station.Infrastructure.Services
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly IConfiguration _config;
        public GoogleAuthService(IConfiguration config)
        {
            _config = config;
        }
        public async Task<Payload> VerifyGoogleTokenAsync(string idToken)
        {

            var settings = new ValidationSettings
            {
                Audience = new[] {
                    _config["Authentication:Google:ClientId"],
                    _config["Authentication:Google:ClientIdPlayGround"]
                }
            };

            return await ValidateAsync(idToken, settings);
        }
    }
}
