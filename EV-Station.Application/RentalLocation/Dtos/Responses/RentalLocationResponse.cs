namespace EV_Station.Application.RentalLocation.Dtos.Responses
{
    public class RentalLocationResponse
    {
        public Guid LocationID { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = string.Empty!;
        public string Email { get; set; } = string.Empty!;
        public string ManagerName { get; set; } = string.Empty!;
        public TimeSpan OpenHour { get; set; }
        public TimeSpan CloseHour { get; set; }
    }
}
