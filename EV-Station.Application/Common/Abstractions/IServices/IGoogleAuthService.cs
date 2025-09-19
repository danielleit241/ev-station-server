using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace EV_Station.Application.Common.Abstractions.IServices
{
    public interface IGoogleAuthService
    {
        Task<Payload> VerifyGoogleTokenAsync(string idToken);
    }
}
