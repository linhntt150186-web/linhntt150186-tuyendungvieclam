using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using JobHubMVC.Models;

namespace JobHubMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure enum conversions
            builder.Entity<ApplicationUser>()
                .Property(e => e.Role)
                .HasConversion<string>();

            builder.Entity<Job>()
                .Property(e => e.Status)
                .HasConversion<string>();

            builder.Entity<Application>()
                .Property(e => e.Status)
                .HasConversion<string>();

            // Configure relationships
            builder.Entity<Job>()
                .HasOne(j => j.Employer)
                .WithMany(u => u.Jobs)
                .HasForeignKey(j => j.EmployerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Application>()
                .HasOne(a => a.Job)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Application>()
                .HasOne(a => a.Applicant)
                .WithMany(u => u.Applications)
                .HasForeignKey(a => a.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique constraint for job application
            builder.Entity<Application>()
                .HasIndex(a => new { a.JobId, a.ApplicantId })
                .IsUnique();

            // Configure Comment relationships
            builder.Entity<Comment>()
                .HasOne(c => c.Job)
                .WithMany()
                .HasForeignKey(c => c.JobId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}