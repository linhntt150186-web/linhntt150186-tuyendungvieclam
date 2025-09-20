using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobHubMVC.Data;
using JobHubMVC.Models;

namespace JobHubMVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Admin)
            {
                return Forbid();
            }

            var stats = new AdminDashboardViewModel
            {
                TotalJobs = await _context.Jobs.CountAsync(),
                ActiveJobs = await _context.Jobs.CountAsync(j => j.Status == JobStatus.Active),
                TotalUsers = await _context.Users.CountAsync(),
                TotalApplications = await _context.Applications.CountAsync(),
                RecentJobs = await _context.Jobs
                    .Include(j => j.Employer)
                    .OrderByDescending(j => j.CreatedAt)
                    .Take(5)
                    .ToListAsync(),
                RecentApplications = await _context.Applications
                    .Include(a => a.Job)
                    .Include(a => a.Applicant)
                    .OrderByDescending(a => a.CreatedAt)
                    .Take(5)
                    .ToListAsync()
            };

            return View(stats);
        }

        public async Task<IActionResult> Jobs()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Admin) return Forbid();

            var jobs = await _context.Jobs
                .Include(j => j.Employer)
                .OrderByDescending(j => j.CreatedAt)
                .ToListAsync();

            return View(jobs);
        }

        public async Task<IActionResult> Users()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Admin) return Forbid();

            var users = await _context.Users
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveJob(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Admin) return Forbid();

            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                job.Status = JobStatus.Active;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã duyệt công việc thành công!";
            }

            return RedirectToAction(nameof(Jobs));
        }

        [HttpPost]
        public async Task<IActionResult> RejectJob(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Admin) return Forbid();

            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                job.Status = JobStatus.Inactive;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã từ chối công việc!";
            }

            return RedirectToAction(nameof(Jobs));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Admin) return Forbid();

            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã xóa công việc!";
            }

            return RedirectToAction(nameof(Jobs));
        }

        public async Task<IActionResult> Employers()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Admin) return Forbid();

            var employers = await _context.Users
                .Where(u => u.Role == UserRole.Employer)
                .Include(u => u.Jobs)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            return View(employers);
        }

        public async Task<IActionResult> JobSeekers()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Admin) return Forbid();

            var jobSeekers = await _context.Users
                .Where(u => u.Role == UserRole.JobSeeker)
                .Include(u => u.Applications)
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            return View(jobSeekers);
        }

        public async Task<IActionResult> Applications()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Admin) return Forbid();

            var applications = await _context.Applications
                .Include(a => a.Job)
                .Include(a => a.Applicant)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return View(applications);
        }

        [HttpPost]
        public async Task<IActionResult> LockUser(string id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Admin) return Forbid();

            var targetUser = await _userManager.FindByIdAsync(id);
            if (targetUser != null && targetUser.Role != UserRole.Admin)
            {
                await _userManager.SetLockoutEndDateAsync(targetUser, DateTimeOffset.UtcNow.AddYears(100));
                TempData["Success"] = "Đã khóa tài khoản!";
            }

            return RedirectToAction(nameof(Users));
        }
    }

    public class AdminDashboardViewModel
    {
        public int TotalJobs { get; set; }
        public int ActiveJobs { get; set; }
        public int TotalUsers { get; set; }
        public int TotalApplications { get; set; }
        public List<Job> RecentJobs { get; set; } = new();
        public List<Application> RecentApplications { get; set; } = new();
    }
}