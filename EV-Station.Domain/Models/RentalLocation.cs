using System.ComponentModel.DataAnnotations;

namespace EV_Station.Domain.Models
{
    public class RentalLocation
    {
        [Key]
        public Guid LocationID { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = string.Empty!;
        public string Email { get; set; } = string.Empty!;
        public string ManagerName { get; set; } = string.Empty!;
        public TimeSpan OpenHour { get; set; }
        public TimeSpan CloseHour { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; } = [];
        public ICollection<Rental> Pickups { get; set; } = [];
        public ICollection<Rental> Returns { get; set; } = [];
        public ICollection<VehicleHistory> VehicleHistories { get; set; } = [];
    }
}
