using EV_Station.Domain.Models.Enums;

namespace EV_Station.Application.DriverLisences.DTOs.Responses
{
    public class DriverLisenceScanResponse
    {
        public string LicenseNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateOnly? DateOfBirth { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public LicenseClass LicenseClass { get; set; }
        public DateOnly? BeginingDate { get; set; }
        public DateTime? ExpiresDate { get; set; }
        public string ClassificationOfMotorVehicles { get; set; } = string.Empty;
    }
}
