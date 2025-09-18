namespace EV_Station.Application.Common.Abstractions.IServices
{
    public interface ITesseractOcrService
    {
        Task<string> ExtractTextFromImageAsync(string imageUrl);
    }
}
