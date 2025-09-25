using EV_Station.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EV_Station.Domain.Models
{
    public class Payment
    {
        [Key]
        public Guid PaymentID { get; set; }
        public Guid RentalID { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }

        public Rental Rental { get; set; } = null!;
    }
}
