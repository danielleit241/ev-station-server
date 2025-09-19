namespace EV_Station.Application.Users.DTOs.Requests
{
    public class UserRefreshTokenDto
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; } = default!;
    }
}
