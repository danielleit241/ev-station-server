using EV_Station.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EV_Station.Domain.Models
{
    public class Vehicle
    {
        [Key]
        public Guid VehicleID { get; set; }
        public Guid TypeID { get; set; }
        public Guid LocationID { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Year { get; set; }
        public int Odometer { get; set; }
        public string FuelType { get; set; } = string.Empty;
        public VehicleStatus Status { get; set; } = VehicleStatus.Available;
        public DateTime InsuranceExpiry { get; set; }

        public VehicleType Type { get; set; } = null!;
        public RentalLocation Location { get; set; } = null!;
        public ICollection<Rental> Rentals { get; set; } = [];
        public ICollection<VehicleHistory> VehicleHistories { get; set; } = [];
        public ICollection<VehicleImage> VehicleImages { get; set; } = [];
    }
}
