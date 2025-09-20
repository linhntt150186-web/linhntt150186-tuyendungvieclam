using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobHubMVC.Data;
using JobHubMVC.Models;

namespace JobHubMVC.Controllers
{
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? keyword, string? location, string? category, string sortBy = "CreatedAt")
        {
            IQueryable<Job> query = _context.Jobs
                .Where(j => j.Status == JobStatus.Active)
                .Include(j => j.Employer);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(j => j.Title.Contains(keyword) || 
                                        j.Company.Contains(keyword) || 
                                        j.Description.Contains(keyword));
            }

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(j => j.Location.Contains(location));
            }

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(j => j.Category.Contains(category));
            }

            query = sortBy switch
            {
                "Title" => query.OrderBy(j => j.Title),
                "Company" => query.OrderBy(j => j.Company),
                "Location" => query.OrderBy(j => j.Location),
                _ => query.OrderByDescending(j => j.CreatedAt)
            };

            var jobs = await query.ToListAsync();

            ViewBag.Keyword = keyword;
            ViewBag.Location = location;
            ViewBag.Category = category;
            ViewBag.SortBy = sortBy;

            return View(jobs);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var job = await _context.Jobs
                .Include(j => j.Employer)
                .FirstOrDefaultAsync(j => j.Id == id);

            if (job == null)
            {
                return NotFound();
            }

            // Load comments
            var comments = await _context.Comments
                .Include(c => c.User)
                .Where(c => c.JobId == id)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            ViewBag.Comments = comments;
            ViewBag.CurrentUserId = User.Identity?.Name != null ? 
                (await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name))?.Id : null;
            ViewBag.IsAdmin = User.Identity?.Name != null ? 
                (await _context.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name))?.Role == UserRole.Admin : false;

            return View(job);
        }
    }
}