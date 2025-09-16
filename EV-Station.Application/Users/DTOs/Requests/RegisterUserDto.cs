namespace EV_Station.Application.Users.DTOs.Requests
{
    public record RegisterUserDto(
        string Email,
        string Password
    );
}
