using Microsoft.AspNetCore.Http;

namespace EV_Station.Application.Common.Abstractions.IServices
{
    public interface ITesseractOcrService
    {
        Task<string> ExtractTextFromImageUrlAsync(string imageUrl);
        Task<string> ExtractTextFromImageFileAsync(IFormFile formFile);
    }
}
