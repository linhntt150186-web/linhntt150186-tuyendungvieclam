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
    public class EmployerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployerController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Employer) return Forbid();

            var myJobs = await _context.Jobs
                .Where(j => j.EmployerId == user.Id)
                .Include(j => j.Applications)
                .OrderByDescending(j => j.CreatedAt)
                .ToListAsync();

            return View(myJobs);
        }

        [HttpGet]
        public async Task<IActionResult> PostJob()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Employer) return Forbid();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PostJob(PostJobViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Employer) return Forbid();

            if (!ModelState.IsValid) return View(model);

            var job = new Job
            {
                Title = model.Title,
                Company = model.Company,
                Description = model.Description,
                Requirements = model.Requirements,
                Salary = model.Salary,
                Location = model.Location,
                Category = model.Category,
                ImageUrl = model.ImageUrl,
                EmployerId = user.Id,
                Status = JobStatus.Pending
            };

            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đăng tin tuyển dụng thành công! Tin đang chờ duyệt.";
            return RedirectToAction(nameof(Dashboard));
        }

        public async Task<IActionResult> Applications(Guid jobId)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Employer) return Forbid();

            var job = await _context.Jobs
                .Include(j => j.Applications)
                .ThenInclude(a => a.Applicant)
                .FirstOrDefaultAsync(j => j.Id == jobId && j.EmployerId == user.Id);

            if (job == null) return NotFound();

            return View(job);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateApplicationStatus(Guid applicationId, ApplicationStatus status)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user?.Role != UserRole.Employer) return Forbid();

            var application = await _context.Applications
                .Include(a => a.Job)
                .FirstOrDefaultAsync(a => a.Id == applicationId && a.Job.EmployerId == user.Id);

            if (application != null)
            {
                application.Status = status;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Đã cập nhật trạng thái đơn ứng tuyển!";
            }

            return RedirectToAction(nameof(Applications), new { jobId = application?.JobId });
        }
    }

    public class PostJobViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề công việc")]
        [Display(Name = "Tiêu đề công việc")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập tên công ty")]
        [Display(Name = "Tên công ty")]
        public string Company { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mô tả công việc")]
        [Display(Name = "Mô tả công việc")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập yêu cầu công việc")]
        [Display(Name = "Yêu cầu công việc")]
        public string Requirements { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mức lương")]
        [Display(Name = "Mức lương")]
        public string Salary { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập địa điểm")]
        [Display(Name = "Địa điểm")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Danh mục")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập URL hình ảnh")]
        [Display(Name = "Hình ảnh công ty")]
        [Url(ErrorMessage = "Vui lòng nhập URL hợp lệ")]
        public string ImageUrl { get; set; } = string.Empty;
    }
}