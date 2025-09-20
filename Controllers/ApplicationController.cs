using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobHubMVC.Data;
using JobHubMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace JobHubMVC.Controllers
{
    [Authorize]
    public class ApplicationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Apply(Guid jobId)
        {
            var job = await _context.Jobs.Include(j => j.Employer).FirstOrDefaultAsync(j => j.Id == jobId);
            if (job == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.JobSeeker)
            {
                TempData["Error"] = "Chỉ người tìm việc mới có thể ứng tuyển.";
                return RedirectToAction("Details", "Jobs", new { id = jobId });
            }

            var existingApplication = await _context.Applications
                .FirstOrDefaultAsync(a => a.JobId == jobId && a.ApplicantId == user.Id);

            if (existingApplication != null)
            {
                TempData["Error"] = "Bạn đã ứng tuyển công việc này rồi.";
                return RedirectToAction("Details", "Jobs", new { id = jobId });
            }

            var model = new ApplyViewModel
            {
                JobId = jobId,
                JobTitle = job.Title,
                Company = job.Company
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Apply(ApplyViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var application = new Application
            {
                JobId = model.JobId,
                ApplicantId = user.Id,
                CoverLetter = model.CoverLetter,
                CvUrl = model.CvUrl,
                Status = ApplicationStatus.Pending
            };

            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Ứng tuyển thành công! Chúng tôi sẽ liên hệ với bạn sớm.";
            return RedirectToAction("Details", "Jobs", new { id = model.JobId });
        }

        public async Task<IActionResult> MyApplications()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var applications = await _context.Applications
                .Include(a => a.Job)
                .Where(a => a.ApplicantId == user.Id)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return View(applications);
        }
    }

    public class ApplyViewModel
    {
        public Guid JobId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập thư giới thiệu")]
        [Display(Name = "Thư giới thiệu")]
        public string CoverLetter { get; set; } = string.Empty;

        [Display(Name = "Link CV (tùy chọn)")]
        [Url(ErrorMessage = "Vui lòng nhập URL hợp lệ")]
        public string? CvUrl { get; set; }
    }
}