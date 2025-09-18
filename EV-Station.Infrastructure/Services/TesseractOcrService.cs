using EV_Station.Application.Common.Abstractions.IServices;
using Tesseract;

namespace EV_Station.Infrastructure.Services
{
    public class TesseractOcrService : ITesseractOcrService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> ExtractTextFromImageAsync(string imageUrl)
        {
            var imageBytes = await _httpClient.GetByteArrayAsync(imageUrl);
            var tempImagePath = Path.GetTempFileName();

            await File.WriteAllBytesAsync(tempImagePath, imageBytes);
            try
            {
                using var engine = new TesseractEngine(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata"),
                    "vie",
                    EngineMode.Default);

                using var img = Pix.LoadFromFile(tempImagePath);
                using var page = engine.Process(img);

                return page.GetText() ?? string.Empty;
            }
            finally
            {
                if (File.Exists(tempImagePath))
                    File.Delete(tempImagePath);
            }
        }
    }
}
