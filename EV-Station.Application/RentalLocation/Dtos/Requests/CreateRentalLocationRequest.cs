namespace EV_Station.Application.RentalLocation.Dtos.Requests
{
    public record CreateRentalLocationRequest
    (
         string Name,
         string Address,
         string Phone,
         string Email,
         string ManagerName,
         TimeSpan OpenHour,
         TimeSpan CloseHour
    );
}
