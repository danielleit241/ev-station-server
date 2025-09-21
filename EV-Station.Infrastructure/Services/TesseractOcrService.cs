using EV_Station.Application.Common.Abstractions.IServices;
using Microsoft.AspNetCore.Http;
using Tesseract;

namespace EV_Station.Infrastructure.Services
{
    public class TesseractOcrService : ITesseractOcrService
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> ExtractTextFromImageFileAsync(IFormFile formFile)
        {
            if (formFile == null || formFile.Length == 0)
                return "File is null or empty.";

            var tempImagePath = Path.GetTempFileName();

            try
            {
                await using (var stream = new FileStream(tempImagePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                try
                {
                    using var engine = new TesseractEngine(
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata"),
                        "vie",
                        EngineMode.Default);

                    using var img = Pix.LoadFromFile(tempImagePath);
                    using var page = engine.Process(img);

                    var text = page.GetText();
                    return string.IsNullOrWhiteSpace(text) ? "No text extracted." : text;
                }
                catch (Exception ex)
                {
                    return $"OCR failed: {ex.Message}";
                }
            }
            finally
            {
                try
                {
                    if (File.Exists(tempImagePath))
                        File.Delete(tempImagePath);
                }
                catch
                {
                }
            }
        }

        public async Task<string> ExtractTextFromImageUrlAsync(string imageUrl)
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
