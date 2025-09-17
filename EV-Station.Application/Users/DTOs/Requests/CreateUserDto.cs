namespace EV_Station.Application.Users.DTOs.Requests
{
    public record CreateUserDto(
            string email,
            string? fullName,
            string? avatarUrl,
            string password,
            string roleName
     );
}
