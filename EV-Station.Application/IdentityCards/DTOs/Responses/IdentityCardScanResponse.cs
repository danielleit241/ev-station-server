namespace EV_Station.Application.IdentityCards.DTOs.Responses
{
    public class IdentityCardScanResponse
    {
        public string CardNumber { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public DateOnly? DateOfBirth { get; set; }
        public string PlaceOfOrigin { get; set; } = string.Empty;
        public string PlaceOfResidence { get; set; } = string.Empty;
        public DateOnly? CreateDate { get; set; }
        public DateOnly? DayOfExpiry { get; set; }
    }
}
