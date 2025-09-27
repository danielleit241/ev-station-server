using EV_Station.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EV_Station.Domain.Models
{
    public class Payment
    {
        [Key]
        public Guid PaymentID { get; set; }
        public Guid RentalID { get; set; }
        public DateTime PaymentDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }

        public Rental Rental { get; set; } = null!;
    }
}
