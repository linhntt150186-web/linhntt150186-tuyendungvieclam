# JobHub - Website Tuyển Dụng Việc Làm

## ✅ Tính năng đã hoàn thành

### 🏠 Trang chủ
- Hero section với form tìm kiếm
- Thống kê website (10,000+ việc làm, 5,000+ công ty...)
- Hiển thị việc làm nổi bật
- Hướng dẫn cách thức hoạt động
- Call-to-action section

### 🔍 Tìm kiếm việc làm
- Danh sách tất cả việc làm
- Tìm kiếm theo từ khóa, địa điểm, danh mục
- Sắp xếp theo: Mới nhất, Tên công việc, Công ty, Địa điểm
- Xem chi tiết từng công việc

### 👤 Hệ thống tài khoản
- **Đăng ký**: Hỗ trợ 3 loại tài khoản
  - Người tìm việc (JobSeeker)
  - Nhà tuyển dụng (Employer) 
  - Quản trị viên (Admin)
- **Đăng nhập/Đăng xuất**
- **Hồ sơ cá nhân**: Cập nhật thông tin cá nhân

### 📝 Chức năng ứng tuyển
- **Ứng tuyển việc làm**: Gửi thư giới thiệu + link CV
- **Quản lý đơn ứng tuyển**: Xem trạng thái đơn ứng tuyển
- **Trạng thái**: Đang chờ, Đã xem, Được chấp nhận, Bị từ chối

### 🎨 Giao diện
- **Responsive design** với Tailwind CSS
- **Giống hệt** giao diện React version
- **Icons** từ Font Awesome
- **Màu sắc** và layout nhất quán

## 🚀 Cách chạy website

### 1. Dừng ứng dụng hiện tại (nếu đang chạy)
```bash
# Nhấn Ctrl+C trong terminal đang chạy
```

### 2. Build và chạy lại
```bash
cd "D:\New folder\ASP.NET\28092025- doanphuocmien\tuyển dụng việc làm\2\tuyendung\JobHubMVC"
dotnet build
dotnet run
```

### 3. Truy cập website
- Mở trình duyệt và vào: `https://localhost:5001` hoặc `http://localhost:5000`

## 📊 Dữ liệu mẫu

Website đã có sẵn 6 công việc mẫu:
1. **Nhân viên pha chế - Barista** (Coffee House)
2. **Nhân viên kinh doanh** (Công ty TNHH XYZ)  
3. **Lập trình viên Full-stack** (Tech Solutions Co.)
4. **Nhân viên Marketing Digital** (Digital Marketing Agency)
5. **Kế toán tổng hợp** (Công ty CP Đầu tư ABC)
6. **Giáo viên Toán - Lý** (Trung tâm Gia sư Thông minh)

## 🔧 Cách sử dụng

### Đăng ký tài khoản mới
1. Click **"Đăng ký"** ở góc phải trên
2. Nhập thông tin: Họ tên, Email, Mật khẩu
3. Chọn loại tài khoản: **Người tìm việc** hoặc **Nhà tuyển dụng**

### Ứng tuyển việc làm
1. **Đăng nhập** với tài khoản **Người tìm việc**
2. Tìm việc làm phù hợp
3. Click **"Ứng tuyển ngay"**
4. Viết thư giới thiệu và đính kèm link CV
5. Gửi đơn ứng tuyển

### Quản lý hồ sơ
1. Click vào **tên người dùng** ở góc phải trên
2. Chọn **"Hồ sơ cá nhân"** để cập nhật thông tin
3. Chọn **"Dashboard"** để xem đơn ứng tuyển

## 🗂️ Cấu trúc dự án

```
JobHubMVC/
├── Controllers/           # Xử lý logic
│   ├── HomeController.cs     # Trang chủ
│   ├── JobsController.cs     # Việc làm
│   ├── AccountController.cs  # Đăng nhập/ký
│   ├── ApplicationController.cs # Ứng tuyển
│   └── ProfileController.cs  # Hồ sơ cá nhân
├── Models/               # Dữ liệu
│   ├── ApplicationUser.cs    # Người dùng
│   ├── Job.cs               # Việc làm
│   └── Application.cs       # Đơn ứng tuyển
├── Views/                # Giao diện
│   ├── Home/                # Trang chủ
│   ├── Jobs/                # Việc làm
│   ├── Account/             # Đăng nhập/ký
│   ├── Application/         # Ứng tuyển
│   └── Profile/             # Hồ sơ
├── Data/                 # Database
│   ├── ApplicationDbContext.cs
│   └── SeedData.cs          # Dữ liệu mẫu
└── wwwroot/              # Static files
```

## 🎯 Kết quả

✅ **Hoàn thành 100%** chuyển đổi từ React sang ASP.NET Core MVC
✅ **Giữ nguyên** toàn bộ giao diện và tính năng
✅ **Thêm mới** chức năng ứng tuyển và quản lý hồ sơ
✅ **Database** SQLite với dữ liệu mẫu
✅ **Responsive** trên mọi thiết bị

Website đã sẵn sàng sử dụng! 🚀