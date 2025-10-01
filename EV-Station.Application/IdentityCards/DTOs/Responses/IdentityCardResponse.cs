using EV_Station.Domain.Models.Enums;

namespace EV_Station.Application.IdentityCards.DTOs.Responses
{
    public class IdentityCardResponse
    {
        public string CardNumber { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Sex { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public DateOnly? CreateDate { get; set; }
        public DateOnly? DayOfExpiry { get; set; }
        public string PlaceOfOrigin { get; set; } = string.Empty;
        public string PlaceOfResidence { get; set; } = string.Empty;
        public string? FrontImagePath { get; set; }
        public string? BackImagePath { get; set; }
        public VerificationStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; set; } = default!;
    }
}
