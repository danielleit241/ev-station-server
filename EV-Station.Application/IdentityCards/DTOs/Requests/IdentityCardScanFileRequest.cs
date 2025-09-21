using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EV_Station.Application.IdentityCards.DTOs.Requests
{
    public class IdentityCardScanFileRequest
    {
        [FromForm(Name = "frontImage")]
        public IFormFile FrontImage { get; set; } = default!;

        [FromForm(Name = "backImage")]
        public IFormFile BackImage { get; set; } = default!;
    }
}
