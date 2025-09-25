using System.ComponentModel.DataAnnotations;

namespace EV_Station.Domain.Models
{
    public class AdditionalCharge
    {
        [Key]
        public Guid AdditionalChargeID { get; set; }
        public Guid RentalID { get; set; }
        public string Reason { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPaid { get; set; }

        public Rental Rental { get; set; } = null!;
    }
}
