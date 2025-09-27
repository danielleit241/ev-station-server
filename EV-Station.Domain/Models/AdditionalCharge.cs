using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EV_Station.Domain.Models
{
    public class AdditionalCharge
    {
        [Key]
        public Guid AdditionalChargeID { get; set; }
        public Guid RentalID { get; set; }
        public string Reason { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPaid { get; set; }

        public Rental Rental { get; set; } = null!;
    }
}
