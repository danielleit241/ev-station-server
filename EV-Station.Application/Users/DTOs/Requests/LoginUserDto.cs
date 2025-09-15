namespace EV_Station.Application.Users.DTOs.Requests
{
    public record LoginUserDto(
        string Email,
        string Password
    );
}
