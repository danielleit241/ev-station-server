using System.ComponentModel.DataAnnotations;

namespace EV_Station.Application.DriverLisences.DTOs.Requests
{
    public class DriverLisenceScanUrlRequest
    {
        [Required]
        public string FrontImageUrl { get; set; } = default!;
        [Required]
        public string BackImageUrl { get; set; } = default!;
    }
}
