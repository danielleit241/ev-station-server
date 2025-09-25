using System.ComponentModel.DataAnnotations;

namespace EV_Station.Domain.Models
{
    public class Policy
    {
        [Key]
        public Guid PolicyId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
}
