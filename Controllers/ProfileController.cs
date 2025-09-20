using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using JobHubMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace JobHubMVC.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var model = new ProfileViewModel
            {
                FullName = user.FullName,
                Email = user.Email!,
                Phone = user.Phone,
                Company = user.Company,
                Role = user.Role
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            user.FullName = model.FullName;
            user.Phone = model.Phone;
            user.Company = model.Company;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["Success"] = "Cập nhật hồ sơ thành công!";
            }
            else
            {
                TempData["Error"] = "Có lỗi xảy ra khi cập nhật hồ sơ.";
            }

            return View(model);
        }
    }

    public class ProfileViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        [Display(Name = "Họ và tên")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [Display(Name = "Số điện thoại")]
        public string? Phone { get; set; }

        [Display(Name = "Công ty")]
        public string? Company { get; set; }

        public UserRole Role { get; set; }
    }
}