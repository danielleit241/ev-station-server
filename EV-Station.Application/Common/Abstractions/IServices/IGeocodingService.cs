using EV_Station.Application.RentalLocation.Dtos.Responses;

namespace EV_Station.Application.Common.Abstractions.IServices
{
    public interface IGeocodingService
    {
        Task<RouteLocationResponse> GetCoordinatesAsync(string userAddress, string rentalLocationAddress);
    }
}
