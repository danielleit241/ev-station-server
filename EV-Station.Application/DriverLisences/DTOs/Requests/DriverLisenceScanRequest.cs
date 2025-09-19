namespace EV_Station.Application.DriverLisences.DTOs.Requests
{
    public class DriverLisenceScanRequest
    {
        public string FrontImageUrl { get; set; } = default!;
        public string BackImageUrl { get; set; } = default!;
    }
}
