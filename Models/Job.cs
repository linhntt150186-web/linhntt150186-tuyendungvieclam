using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobHubMVC.Models
{
    public class Job
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Company { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Requirements { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Salary { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public string EmployerId { get; set; } = string.Empty;

        public JobStatus Status { get; set; } = JobStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [ForeignKey("EmployerId")]
        public virtual ApplicationUser Employer { get; set; } = null!;
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}