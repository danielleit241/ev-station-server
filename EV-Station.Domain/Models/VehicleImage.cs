using System.ComponentModel.DataAnnotations;

namespace EV_Station.Domain.Models
{
    public class VehicleImage
    {
        [Key]
        public Guid VehicleImageID { get; set; }
        public Guid VehicleID { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string? Description { get; set; }

        public Vehicle Vehicle { get; set; } = null!;
    }
}
