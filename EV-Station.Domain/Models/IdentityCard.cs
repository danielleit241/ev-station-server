using EV_Station.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace EV_Station.Domain.Models
{
    public class IdentityCard
    {
        [Key]
        public string CardNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string PlaceOfOrigin { get; set; } = string.Empty;
        public string PlaceOfResidence { get; set; } = string.Empty;
        public DateOnly CreateDate { get; set; }
        public DateOnly DayOfExpiry { get; set; }
        public string FrontImagePath { get; set; } = string.Empty;
        public string BackImagePath { get; set; } = string.Empty;

        public VerificationStatus Status { get; set; } = VerificationStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
