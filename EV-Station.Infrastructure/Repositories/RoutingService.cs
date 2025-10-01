using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using System.Net.Http.Json;

namespace EV_Station.Infrastructure.Repositories
{
    public class RoutingService : IRoutingService
    {
        private readonly HttpClient _httpClient;
        public RoutingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OSRMRoute> GetRouteAsync(LocationMarkerResponse origin, LocationMarkerResponse destination)
        {
            var url = $"http://router.project-osrm.org/route/v1/driving/{origin.Longitude},{origin.Latitude};{destination.Longitude},{destination.Latitude}?overview=full&geometries=geojson";

            var response = await _httpClient.GetFromJsonAsync<OSRMRouteResponse>(url);

            var route = response?.Routes?.FirstOrDefault();
            if (route == null)
                throw new Exception("Could not find route");
            return route;
        }
    }
}
