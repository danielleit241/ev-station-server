namespace EV_Station.Application.Users.DTOs.Response
{
    public record UserResponseDto(
        Guid Id,
        string Email,
        string FullName,
        string AvatarUrl,
        string Role
    );
}
