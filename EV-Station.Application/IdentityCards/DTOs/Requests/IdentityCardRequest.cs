namespace EV_Station.Application.IdentityCards.DTOs.Requests
{
    public class IdentityCardRequest
    {
        public string CardNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string PlaceOfOrigin { get; set; } = string.Empty;
        public string PlaceOfResidence { get; set; } = string.Empty;
        public DateOnly CreateDate { get; set; }
        public DateOnly DayOfExpiry { get; set; }
        public string? FrontImageUrl { get; set; }
        public string? BackImageUrl { get; set; }

    }
}
