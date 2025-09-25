using EV_Station.Domain.Models.Enums;

namespace EV_Station.Domain.Models
{
    public class RentalImage
    {
        public Guid RentalImageID { get; set; }
        public Guid RentalID { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public RentalStage Stage { get; set; }
        public DateTime UploadedAt { get; set; }

        public Rental Rental { get; set; } = null!;
    }
}
