using System.ComponentModel.DataAnnotations;

namespace EV_Station.Domain.Models
{
    public class VehicleType
    {
        [Key]
        public Guid TypeID { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal PricePerHour { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal DepositAmount { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = [];
    }
}
