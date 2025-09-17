namespace EV_Station.Application.Users.DTOs.Requests
{

    public record UpdateUserDto(
            string? Email,
            string? FullName,
            string? AvatarUrl
        );
}
