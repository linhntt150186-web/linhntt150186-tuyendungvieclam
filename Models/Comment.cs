using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobHubMVC.Models
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid JobId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("JobId")]
        public virtual Job Job { get; set; } = null!;

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; } = null!;
    }
}