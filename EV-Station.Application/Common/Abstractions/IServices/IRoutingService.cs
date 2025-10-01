using EV_Station.Application.RentalLocation.Dtos.Responses;

namespace EV_Station.Application.Common.Abstractions.IServices
{
    public interface IRoutingService
    {
        public Task<OSRMRoute> GetRouteAsync(LocationResponse oirgin, LocationResponse destination);
    }
}
