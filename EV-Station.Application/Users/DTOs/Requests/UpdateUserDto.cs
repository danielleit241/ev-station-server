namespace EV_Station.Application.Users.DTOs.Requests
{

    public record UpdateUserDto(
            string? FullName,
            string? AvatarUrl
        );
}
