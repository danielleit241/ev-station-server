using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EV_Station.Application.DriverLisences.DTOs.Requests
{
    public class DriverLisenceScanFileRequest
    {
        [FromForm(Name = "frontImage")]
        [Required]
        public IFormFile FrontImage { get; set; } = default!;
        [FromForm(Name = "backImage")]
        [Required]
        public IFormFile BackImage { get; set; } = default!;
    }
}
