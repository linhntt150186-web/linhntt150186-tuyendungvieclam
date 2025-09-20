using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobHubMVC.Models
{
    public class Application
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid JobId { get; set; }

        [Required]
        public string ApplicantId { get; set; } = string.Empty;

        [Required]
        public string CoverLetter { get; set; } = string.Empty;

        public string? CvUrl { get; set; }

        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("JobId")]
        public virtual Job Job { get; set; } = null!;

        [ForeignKey("ApplicantId")]
        public virtual ApplicationUser Applicant { get; set; } = null!;
    }
}