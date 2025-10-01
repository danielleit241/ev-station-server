using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using System.Globalization;
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

        public async Task<LocationMarkerResponse> GetCoordinatesAsync(string address)
        {
            var url = $"https://nominatim.openstreetmap.org/search?format=json&q={Uri.EscapeDataString(address)}&countrycodes=VN&limit=1";
            var response = await _httpClient.GetFromJsonAsync<List<NominatimResponse>>(url);

            if (response == null || response.Count == 0)
            {
                throw new Exception($"Could not find coordinates for address: {address}");
            }

            var result = response[0];
            return new LocationMarkerResponse
            (
                Address: result.DisplayName,
                Latitude: double.Parse(result.Lat, CultureInfo.InvariantCulture),
                Longitude: double.Parse(result.Lon, CultureInfo.InvariantCulture)
            );
        }
    }
}
