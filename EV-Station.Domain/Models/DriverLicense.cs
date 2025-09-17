using System.ComponentModel.DataAnnotations;

namespace EV_Station.Domain.Models
{
    public class DriverLicense
    {
        [Key]
        public string LicenseNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public LicenseClass LicenseClass { get; set; }
        public DateTime ExpiresDate { get; set; }
        public string FrontImagePath { get; set; } = string.Empty;
        public string BackImagePath { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }

    public enum LicenseClass
    {
        A1,
        A2,
        B1,
        B2,
        C,
        D
    }
}
