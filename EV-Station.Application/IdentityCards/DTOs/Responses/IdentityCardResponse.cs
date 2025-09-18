namespace EV_Station.Application.IdentityCards.DTOs.Responses
{
    public class IdentityCardResponse
    {
        //public long Id { get; set; }
        //public string CardNumber { get; set; } = default!;
        //public string FullName { get; set; } = default!;
        //public DateTime DateOfBirth { get; set; }
        //public DateTime? ExpiryDate { get; set; }
        //public string? FrontImageUrl { get; set; }
        //public string? BackImageUrl { get; set; }
        //public VerificationStatus Status { get; set; }

        public string FrontText { get; set; } = default!;
        public string BackText { get; set; } = default!;

    }
}
