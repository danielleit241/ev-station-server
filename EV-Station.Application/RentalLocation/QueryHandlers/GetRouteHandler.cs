using EV_Station.Application.Common.Abstractions.IServices;
using EV_Station.Application.Common.Responses;
using EV_Station.Application.RentalLocation.Dtos.Responses;
using EV_Station.Application.RentalLocation.Queries;
using MediatR;

namespace EV_Station.Application.RentalLocation.QueryHandlers
{
    public class GetRouteHandler : IRequestHandler<GetRoute, GenericApiResponse<RouteLocationResponse>>
    {
        private readonly IGeocodingService _geocoding;
        private readonly IRoutingService _routing;
        public GetRouteHandler(IGeocodingService geocoding, IRoutingService routing)
        {
            _geocoding = geocoding;
            _routing = routing;
        }
        public async Task<GenericApiResponse<RouteLocationResponse>> Handle(GetRoute request, CancellationToken cancellationToken)
        {
            var userCoordinates = await _geocoding.GetCoordinatesAsync(request.dto.UserAddress);
            var rentalCoordinates = await _geocoding.GetCoordinatesAsync(request.dto.RentalLocationAddress);


            if (userCoordinates == null || rentalCoordinates == null)
            {
                return GenericApiResponse<RouteLocationResponse>.FailResponse("Could not find coordinates for one or both addresses.");
            }

            var route = await _routing.GetRouteAsync(userCoordinates, rentalCoordinates);

            var result = new RouteLocationResponse(
                UserLocation: userCoordinates,
                RentalLocation: rentalCoordinates,
                DistanceKm: route.Distance / 1000.0,
                DurationMinutes: route.Duration / 60.0,
                Polyline: route.Geometry
            );

            return GenericApiResponse<RouteLocationResponse>.SuccessResponse(result);
        }
    }
}
