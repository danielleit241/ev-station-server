using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EV_Station.Domain.Models
{
    public class VehicleType
    {
        [Key]
        public Guid TypeID { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerHour { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerDay { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DepositAmount { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = [];
    }
}
