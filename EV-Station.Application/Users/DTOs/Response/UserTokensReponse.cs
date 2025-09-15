namespace EV_Station.Application.Users.DTOs.Response
{
    public class UserTokensReponse
    {
        public UserResponseDto User { get; set; } = null!;
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
