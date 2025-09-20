using Microsoft.AspNetCore.Identity;
using JobHubMVC.Models;

namespace JobHubMVC.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Create roles
            string[] roles = { "Admin", "Employer", "JobSeeker" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create admin user
            var adminEmail = "admin@jobhub.vn";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FullName = "Admin JobHub",
                    Role = UserRole.Admin,
                    Company = "JobHub",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "Admin123!");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Create employer user
            var employerEmail = "employer@jobhub.vn";
            var employerUser = await userManager.FindByEmailAsync(employerEmail);
            if (employerUser == null)
            {
                employerUser = new ApplicationUser
                {
                    UserName = employerEmail,
                    Email = employerEmail,
                    FullName = "Nhà tuyển dụng Demo",
                    Role = UserRole.Employer,
                    Company = "Công ty Demo",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(employerUser, "Employer123!");
                await userManager.AddToRoleAsync(employerUser, "Employer");
            }

            // Create sample jobs if none exist
            if (!context.Jobs.Any())
            {
                var sampleJobs = new[]
                {
                    new Job
                    {
                        Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                        Title = "Nhân viên pha chế - Barista",
                        Company = "Coffee House",
                        Description = "Chúng tôi đang tìm kiếm nhân viên pha chế nhiệt huyết, yêu thích cà phê và có khả năng giao tiếp tốt với khách hàng. Bạn sẽ được đào tạo kỹ thuật pha chế chuyên nghiệp và làm việc trong môi trường năng động, thân thiện.",
                        Requirements = "- Tốt nghiệp THPT trở lên\n- Yêu thích cà phê và đồ uống\n- Kỹ năng giao tiếp tốt\n- Có thể làm việc theo ca\n- Chịu được áp lực công việc",
                        Salary = "7.000.000 - 9.000.000 VNĐ",
                        Location = "TP. Hồ Chí Minh",
                        Category = "Dịch vụ - Nhà hàng",
                        ImageUrl = "https://images.pexels.com/photos/312418/pexels-photo-312418.jpeg?auto=compress&cs=tinysrgb&w=400",
                        EmployerId = adminUser.Id,
                        Status = JobStatus.Active
                    },
                    new Job
                    {
                        Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                        Title = "Nhân viên kinh doanh - Sales Executive",
                        Company = "Công ty TNHH XYZ",
                        Description = "Tìm kiếm nhân viên kinh doanh có kinh nghiệm, khả năng đàm phán tốt và đam mê với công việc bán hàng. Bạn sẽ được hỗ trợ đầy đủ về sản phẩm và có cơ hội thăng tiến cao.",
                        Requirements = "- Kinh nghiệm 1-2 năm trong lĩnh vực sales\n- Kỹ năng đàm phán và thuyết phục\n- Có xe máy, biết lái xe\n- Chăm chỉ, năng động\n- Khả năng chịu áp lực cao",
                        Salary = "8.000.000 - 15.000.000 VNĐ + Hoa hồng",
                        Location = "Hà Nội",
                        Category = "Kinh doanh - Bán hàng",
                        ImageUrl = "https://images.pexels.com/photos/7681045/pexels-photo-7681045.jpeg?auto=compress&cs=tinysrgb&w=400",
                        EmployerId = adminUser.Id,
                        Status = JobStatus.Active
                    },
                    new Job
                    {
                        Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                        Title = "Lập trình viên Full-stack",
                        Company = "Tech Solutions Co.",
                        Description = "Chúng tôi đang tìm kiếm lập trình viên full-stack để tham gia các dự án web application và mobile app. Bạn sẽ làm việc với công nghệ hiện đại và team developer giàu kinh nghiệm.",
                        Requirements = "- Tốt nghiệp ĐH chuyên ngành CNTT\n- Kinh nghiệm 2+ năm với React, Node.js\n- Biết TypeScript, MongoDB\n- Có kinh nghiệm API development\n- Kỹ năng làm việc nhóm tốt",
                        Salary = "15.000.000 - 25.000.000 VNĐ",
                        Location = "Hà Nội",
                        Category = "Công nghệ thông tin",
                        ImageUrl = "https://images.pexels.com/photos/574071/pexels-photo-574071.jpeg?auto=compress&cs=tinysrgb&w=400",
                        EmployerId = adminUser.Id,
                        Status = JobStatus.Active
                    },
                    new Job
                    {
                        Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                        Title = "Nhân viên Marketing Digital",
                        Company = "Digital Marketing Agency",
                        Description = "Tìm kiếm nhân viên marketing digital có khả năng lập kế hoạch và triển khai các chiến dịch trực tuyến hiệu quả. Môi trường làm việc sáng tạo và năng động.",
                        Requirements = "- Tốt nghiệp ĐH Marketing/Kinh tế\n- Kinh nghiệm với Google Ads, Facebook Ads\n- Biết SEO/SEM cơ bản\n- Sáng tạo, năng động\n- Kỹ năng phân tích dữ liệu",
                        Salary = "8.000.000 - 14.000.000 VNĐ",
                        Location = "TP. Hồ Chí Minh",
                        Category = "Marketing - PR",
                        ImageUrl = "https://images.pexels.com/photos/1181467/pexels-photo-1181467.jpeg?auto=compress&cs=tinysrgb&w=400",
                        EmployerId = adminUser.Id,
                        Status = JobStatus.Active
                    },
                    new Job
                    {
                        Id = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                        Title = "Kế toán tổng hợp",
                        Company = "Công ty CP Đầu tư ABC",
                        Description = "Tuyển kế toán tổng hợp có kinh nghiệm, làm việc tại văn phòng hiện đại, chế độ đảm bảo. Cơ hội học hỏi và phát triển nghề nghiệp.",
                        Requirements = "- Tốt nghiệp CĐ/ĐH chuyên ngành Kế toán\n- Kinh nghiệm 2-3 năm\n- Thành thạo Excel, phần mềm kế toán\n- Tỉ mỉ, cẩn thận\n- Có chứng chỉ kế toán ưu tiên",
                        Salary = "10.000.000 - 15.000.000 VNĐ",
                        Location = "TP. Hồ Chí Minh",
                        Category = "Tài chính - Kế toán",
                        ImageUrl = "https://images.pexels.com/photos/6801871/pexels-photo-6801871.jpeg?auto=compress&cs=tinysrgb&w=400",
                        EmployerId = adminUser.Id,
                        Status = JobStatus.Active
                    },
                    new Job
                    {
                        Id = Guid.Parse("10000000-0000-0000-0000-000000000006"),
                        Title = "Giáo viên Toán - Lý",
                        Company = "Trung tâm Gia sư Thông minh",
                        Description = "Tuyển giáo viên dạy Toán, Lý các cấp từ THCS đến THPT, có passion với việc giảng dạy. Môi trường giáo dục chuyên nghiệp và hiện đại.",
                        Requirements = "- Tốt nghiệp ĐH chuyên ngành Toán/Lý\n- Có kinh nghiệm giảng dạy\n- Kỹ năng truyền đạt tốt\n- Yêu thương học sinh\n- Có bằng sư phạm ưu tiên",
                        Salary = "12.000.000 - 20.000.000 VNĐ",
                        Location = "Hà Nội",
                        Category = "Giáo dục - Đào tạo",
                        ImageUrl = "https://images.pexels.com/photos/8197543/pexels-photo-8197543.jpeg?auto=compress&cs=tinysrgb&w=400",
                        EmployerId = adminUser.Id,
                        Status = JobStatus.Active
                    }
                };

                context.Jobs.AddRange(sampleJobs);
                await context.SaveChangesAsync();
            }
        }
    }
}