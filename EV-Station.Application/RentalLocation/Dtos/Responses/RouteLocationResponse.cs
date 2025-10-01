namespace EV_Station.Application.RentalLocation.Dtos.Responses
{
    public record RouteLocationResponse(
        UserLocationResponse UserLocation,
        RentalLocationResponse RentalLocation
    );

    public record UserLocationResponse(
        string UserAddress,
        double UserLatitude,
        double UserLongitude
    );

    public record RentalLocationResponse(
        string RentalLocationAddress,
        double RentalLocationLatitude,
        double RentalLocationLongitude
    );
}
