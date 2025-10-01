namespace EV_Station.Application.RentalLocation.Dtos.Responses
{
    public record OSRMRoute(double Distance, double Duration, Geometry Geometry);
    public record Geometry(string Type, List<List<double>> Coordinates);
    public record OSRMRouteResponse(List<OSRMRoute> Routes);
}
