namespace EV_Station.Application.IdentityCards.DTOs.Requests
{
    public class IdentityCardScanRequest
    {
        public string FrontImageUrl { get; set; } = default!;
        public string BackImageUrl { get; set; } = default!;
    }
}
