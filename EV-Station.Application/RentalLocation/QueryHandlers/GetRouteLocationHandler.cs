using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using EV_Station.Application.RentalLocation.Queries;
using MediatR;

namespace EV_Station.Application.RentalLocation.QueryHandlers
{
    public class GetRouteLocationHandler : IRequestHandler<GetRouteLocation, GenericApiResponse<RouteLocationResponse>>
    {
        private readonly IGeocodingService _geocoding;
        public GetRouteLocationHandler(IGeocodingService geocoding)
        {
            _geocoding = geocoding;
        }
        public async Task<GenericApiResponse<RouteLocationResponse>> Handle(GetRouteLocation request, CancellationToken cancellationToken)
        {
            var result = await _geocoding.GetCoordinatesAsync(request.dto.UserAddress, request.dto.RentalLocationAddress);

            if (result.UserLocation == null || result.RentalLocation == null)
            {
                return GenericApiResponse<RouteLocationResponse>.FailResponse("Could not find coordinates for one or both addresses.");
            }
            return GenericApiResponse<RouteLocationResponse>.SuccessResponse(result);
        }
    }
}
