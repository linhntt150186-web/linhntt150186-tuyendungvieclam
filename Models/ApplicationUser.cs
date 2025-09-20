using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JobHubMVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public UserRole Role { get; set; } = UserRole.JobSeeker;

        [Phone]
        public string? Phone { get; set; }

        [StringLength(200)]
        public string? Company { get; set; }

        public string? AvatarUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}