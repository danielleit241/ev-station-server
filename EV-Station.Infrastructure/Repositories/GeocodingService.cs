using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace EV_Station.Infrastructure.Repositories
{
    public class NominatimResponse
    {
        [JsonPropertyName("lat")]
        public string Lat { get; set; } = string.Empty;

        [JsonPropertyName("lon")]
        public string Lon { get; set; } = string.Empty;

        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
    }


    public class GeocodingService : IGeocodingService
    {

        private readonly HttpClient _httpClient;
        public GeocodingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "EV-Station-App");
        }

        private string GetUrl(string address)
        {
            var url = $"https://nominatim.openstreetmap.org/search?format=json&q={Uri.EscapeDataString(address)}&countrycodes=VN&limit=1";
            return url;
        }

        public async Task<RouteLocationResponse> GetCoordinatesAsync(string userAddress, string rentalLocationAddress)
        {
            var userUrl = GetUrl(userAddress);
            var rentalUrl = GetUrl(rentalLocationAddress);

            var userResponseTask = await _httpClient.GetFromJsonAsync<List<NominatimResponse>>(userUrl);
            var rentalResponseTask = await _httpClient.GetFromJsonAsync<List<NominatimResponse>>(rentalUrl);

            if (userResponseTask == null || userResponseTask.Count == 0)
            {
                throw new Exception($"Could not find coordinates for user address: {userAddress}");
            }

            if (rentalResponseTask == null || rentalResponseTask.Count == 0)
            {
                throw new Exception($"Could not find coordinates for rental location address: {rentalLocationAddress}");
            }

            var userLocation = userResponseTask[0];
            Console.WriteLine(userLocation);
            var rentalLocation = rentalResponseTask[0];
            Console.WriteLine(rentalLocation);
            return new RouteLocationResponse(
                new UserLocationResponse(
                    userLocation.DisplayName ?? userAddress,
                    double.Parse(userLocation.Lat),
                    double.Parse(userLocation.Lon)
                ),
                new RentalLocationResponse(
                    rentalLocation.DisplayName ?? rentalLocationAddress,
                    double.Parse(rentalLocation.Lat),
                    double.Parse(rentalLocation.Lon)
                )
            );
        }
    }
}
