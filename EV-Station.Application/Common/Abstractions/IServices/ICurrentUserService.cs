namespace EV_Station.Application.Common.Abstractions.IServices
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }
        string? Claim(string claimType);
    }
}
