using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.IdentityCards.DTOs.Responses;

namespace EV_Station.Application.Common.Abstractions.IServices
{
    public interface IGeminiAiService
    {
        Task<DriverLisenceScanResponse?> ExtractDriverLisenceInfoAsync(string rawOcrText);
        Task<IdentityCardScanResponse?> ExtractIdentityCardInfoAsync(string rawOcrText);

    }
}
