using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses.Gemini;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace EV_Station.Infrastructure.Services.GeminiAi
{
    public class GeminiAiService : IGeminiAiService
    {

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _apiKey;
        private readonly string _endpoint;

        public GeminiAiService(IConfiguration configuration)
        {
            _apiKey = configuration["GeminiAi:ApiKey"]!;
            _endpoint = configuration["GeminiAi:Endpoint"]!;
        }

        public async Task<IdentityCardScanResponse?> ExtractIdentityCardInfoAsync(string rawOcrText)
        {
            var prompt = Prompts.IdentityCardPrompt(rawOcrText);
            var responseJson = await QueryGeminiAiAsync(prompt);
            var identityCard = JsonSerializer.Deserialize<IdentityCardScanResponse>(responseJson, JsonOptions());
            if (identityCard == null)
                return null;
            return identityCard;
        }

        public async Task<string> QueryGeminiAiAsync(string prompt)
        {
            var requestObj = PrepareRequest(prompt);

            _httpClient.DefaultRequestHeaders.Add("X-goog-api-key", _apiKey);
            var content = new StringContent(
                JsonSerializer.Serialize(requestObj),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(_endpoint, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            string extractedJson = ExtractJson(responseBody);

            Console.WriteLine(extractedJson);

            if (string.IsNullOrWhiteSpace(extractedJson))
                return null!;

            return RemoveCodeFence(extractedJson);
        }

        private static GeminiRequest PrepareRequest(string prompt)
        {
            return new GeminiRequest
            {
                contents =
                    [
                        new GeminiContent
                            {
                                parts =
                                    [
                                        new GeminiPart
                                            {
                                                text = prompt,
                                            }
                                    ]
                            }
                    ]
            };
        }

        public static string ExtractJson(string responseBody)
        {
            var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseBody);

            var extractedJson = geminiResponse?.candidates?.FirstOrDefault()?.content?.parts?.FirstOrDefault()?.text;
            if (string.IsNullOrWhiteSpace(extractedJson))
                return null!;

            extractedJson = RemoveCodeFence(extractedJson);

            return extractedJson;
        }


        private static string RemoveCodeFence(string text)
        {
            text = text.Trim();
            if (text.StartsWith("```json"))
            {
                text = text.Substring(7);
                int endIndex = text.LastIndexOf("```");
                if (endIndex != -1)
                    text = text.Substring(0, endIndex);
            }
            return text.Trim();
        }

        private JsonSerializerOptions JsonOptions() => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
}
