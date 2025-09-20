using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobHubMVC.Data;
using JobHubMVC.Models;

namespace JobHubMVC.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid jobId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                TempData["Error"] = "Vui lòng nhập nội dung bình luận.";
                return RedirectToAction("Details", "Jobs", new { id = jobId });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var comment = new Comment
            {
                JobId = jobId,
                UserId = user.Id,
                Content = content.Trim()
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã thêm bình luận thành công!";
            return RedirectToAction("Details", "Jobs", new { id = jobId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return NotFound();

            // Chỉ cho phép xóa bình luận của chính mình hoặc admin
            if (comment.UserId != user.Id && user.Role != UserRole.Admin)
            {
                return Forbid();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đã xóa bình luận!";
            return RedirectToAction("Details", "Jobs", new { id = comment.JobId });
        }
    }
}