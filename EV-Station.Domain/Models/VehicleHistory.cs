using EV_Station.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EV_Station.Domain.Models
{
    public class VehicleHistory
    {
        [Key]
        public Guid HistoryID { get; set; }
        public Guid VehicleID { get; set; }
        public Guid LocationID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public VehicleHistoryReason Reason { get; set; } = VehicleHistoryReason.Pickup;
        public Vehicle Vehicle { get; set; } = null!;
        public RentalLocation Location { get; set; } = null!;
    }
}
