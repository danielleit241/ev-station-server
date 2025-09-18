using EV_Station.Application.IdentityCards.DTOs.Responses;

namespace EV_Station.Application.Common.Abstractions.IServices
{
    public interface IGeminiAiService
    {
        Task<IdentityCardScanResponse?> ExtractIdentityCardInfoAsync(string rawOcrText);

    }
}
