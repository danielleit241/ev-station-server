using EV_Station.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EV_Station.Domain.Models
{
    public class Rental
    {
        [Key]
        public Guid RentalID { get; set; }
        public Guid UserId { get; set; }
        public Guid VehicleID { get; set; }
        public Guid PickupLocationID { get; set; }
        public Guid ReturnLocationID { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public RentalStatus Status { get; set; } = RentalStatus.Rented;
        [Column(TypeName = "decimal(18,2)")]
        public decimal DepositAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LateFee { get; set; }
        public string EmployeeName { get; set; } = string.Empty;

        public User User { get; set; } = null!;
        public Vehicle Vehicle { get; set; } = null!;
        public RentalLocation? PickupLocation { get; set; }
        public RentalLocation? ReturnLocation { get; set; }
        public ICollection<Payment> Payments { get; set; } = [];
        public ICollection<RentalImage> RentalImages { get; set; } = [];
        public ICollection<AdditionalCharge> AdditionalCharges { get; set; } = [];
    }
}
