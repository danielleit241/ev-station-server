namespace EV_Station.Application.RentalLocation.Dtos.Responses
{
    public record RouteResponse(
        LocationMarkerResponse UserLocation,
        LocationMarkerResponse RentalLocation,
        double DistanceKm,
        double DurationMinutes,
        Geometry Polyline
    );
}
