using System.ComponentModel.DataAnnotations;

namespace EV_Station.Domain.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;
        public string? FullName { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; } = string.Empty;
        public string? PasswordHash { get; set; } = string.Empty;

        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = null!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
