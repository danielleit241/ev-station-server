using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses.Gemini;
using EV_Station.Application.DriverLisences.DTOs.Responses;
using EV_Station.Application.IdentityCards.DTOs.Responses;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            try
            {
                var prompt = Prompts.IdentityCardPrompt(rawOcrText);
                var responseJson = await QueryGeminiAiAsync(prompt);
                if (responseJson == null)
                    return null;
                var identityCard = JsonSerializer.Deserialize<IdentityCardScanResponse>(responseJson, JsonOptions());
                if (identityCard == null || !IsValidIdentityCard(identityCard))
                    return null;
                return identityCard;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static bool IsValidIdentityCard(IdentityCardScanResponse data, int maxNullAllowed = 3)
        {
            if (data == null)
                return false;
            int nullOrEmptyCount = 0;

            if (string.IsNullOrWhiteSpace(data.CardNumber)) return false;
            if (string.IsNullOrWhiteSpace(data.FullName)) nullOrEmptyCount++;
            if (string.IsNullOrWhiteSpace(data.Sex)) nullOrEmptyCount++;
            if (string.IsNullOrWhiteSpace(data.Nationality)) nullOrEmptyCount++;
            if (string.IsNullOrWhiteSpace(data.PlaceOfOrigin)) nullOrEmptyCount++;
            if (string.IsNullOrWhiteSpace(data.PlaceOfResidence)) nullOrEmptyCount++;
            if (!data.DateOfBirth.HasValue) nullOrEmptyCount++;
            if (data.CreateDate == default) nullOrEmptyCount++;
            if (data.DayOfExpiry == default) nullOrEmptyCount++;

            return nullOrEmptyCount <= maxNullAllowed;
        }

        public async Task<DriverLisenceScanResponse?> ExtractDriverLisenceInfoAsync(string rawOcrText)
        {
            try
            {
                var prompt = Prompts.DriverLisencePrompt(rawOcrText);
                var responseJson = await QueryGeminiAiAsync(prompt);
                var driverLisence = JsonSerializer.Deserialize<DriverLisenceScanResponse>(responseJson, JsonOptions());
                if (driverLisence == null)
                    return null;
                return driverLisence;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
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
            Console.WriteLine(responseBody);

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
            if (string.IsNullOrWhiteSpace(text))
                return "";

            text = text.Trim();
            if (text.StartsWith("```"))
            {
                int firstLineEnd = text.IndexOf('\n');
                if (firstLineEnd != -1)
                    text = text.Substring(firstLineEnd).Trim();

                int endIndex = text.LastIndexOf("```");
                if (endIndex != -1)
                    text = text.Substring(0, endIndex).Trim();
            }
            text = text.Trim('`', '\n', '\r', ' ');

            return text;
        }


        private JsonSerializerOptions JsonOptions() => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
        };

    }
}
