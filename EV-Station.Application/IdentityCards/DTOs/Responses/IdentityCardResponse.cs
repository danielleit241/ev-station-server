using EV_Station.Domain.Models.Enums;

namespace EV_Station.Application.IdentityCards.DTOs.Responses
{
    public class IdentityCardResponse
    {
        public long Id { get; set; }
        public string CardNumber { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }
        public DateOnly? CreateDate { get; set; }
        public DateOnly? DayOfExpiry { get; set; }
        public string PlaceOfOrigin { get; set; } = string.Empty;
        public string PlaceOfResidence { get; set; } = string.Empty;
        public string? FrontImageUrl { get; set; }
        public string? BackImageUrl { get; set; }
        public VerificationStatus Status { get; set; }
    }
}
