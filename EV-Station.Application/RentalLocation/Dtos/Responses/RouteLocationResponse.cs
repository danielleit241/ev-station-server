namespace EV_Station.Application.RentalLocation.Dtos.Responses
{
    public record RouteLocationResponse(
        LocationResponse UserLocation,
        LocationResponse RentalLocation,
        double DistanceKm,
        double DurationMinutes,
        Geometry Polyline
    );
}
