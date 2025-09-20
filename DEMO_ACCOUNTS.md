# 🔐 THÔNG TIN TÀI KHOẢN DEMO - JOBHUB MVC

## 📋 Danh sách tài khoản để test hệ thống

### 👑 **ADMIN - Quản trị viên**
```
Email: admin@jobhub.vn
Password: Admin123!
Vai trò: Quản trị hệ thống
```
**Quyền hạn:**
- Quản lý tất cả người dùng
- Duyệt/từ chối việc làm
- Xem thống kê tổng quan
- Quản lý đơn ứng tuyển
- Truy cập tất cả chức năng admin

---

### 🏢 **EMPLOYER - Nhà tuyển dụng**
```
Email: employer@jobhub.vn
Password: Employer123!
Vai trò: Nhà tuyển dụng
```
**Quyền hạn:**
- Đăng tin tuyển dụng
- Quản lý việc làm đã đăng
- Xem và duyệt đơn ứng tuyển
- Dashboard nhà tuyển dụng
- Quản lý hồ sơ công ty

---

### 👤 **JOB SEEKER - Ứng viên**
```
Tạo tài khoản mới qua trang Register
Hoặc đăng ký với email bất kỳ
```
**Quyền hạn:**
- Tìm kiếm việc làm
- Ứng tuyển vào các vị trí
- Quản lý đơn ứng tuyển
- Cập nhật hồ sơ cá nhân
- Bình luận trên việc làm

---

## 🚀 **HƯỚNG DẪN SỬ DỤNG**

### 1. Khởi động ứng dụng:
```bash
cd JobHubMVC
dotnet run
```

### 2. Truy cập website:
```
http://localhost:5073
hoặc
https://localhost:7073
```

### 3. Đăng nhập:
- Vào trang `/Account/Login`
- Sử dụng một trong các tài khoản demo ở trên
- Hoặc click nút "Tự động điền" trên trang đăng nhập

---

## 🎯 **TÍNH NĂNG CHÍNH CẦN TEST**

### ✅ **Với tài khoản Admin:**
- [ ] Dashboard admin (`/Admin`)
- [ ] Quản lý người dùng (`/Admin/Users`)
- [ ] Quản lý việc làm (`/Admin/Jobs`)
- [ ] Quản lý đơn ứng tuyển (`/Admin/Applications`)
- [ ] Thống kê hệ thống

### ✅ **Với tài khoản Employer:**
- [ ] Dashboard nhà tuyển dụng (`/Employer/Dashboard`)
- [ ] Đăng việc làm mới (`/Employer/PostJob`)
- [ ] Quản lý đơn ứng tuyển (`/Employer/Applications`)
- [ ] Cập nhật thông tin công ty

### ✅ **Với tài khoản JobSeeker:**
- [ ] Tìm kiếm việc làm (`/Jobs`)
- [ ] Xem chi tiết việc làm (`/Jobs/Details/{id}`)
- [ ] Ứng tuyển (`/Application/Apply`)
- [ ] Quản lý đơn ứng tuyển (`/Application/MyApplications`)
- [ ] Cập nhật hồ sơ (`/Profile`)

---

## 🔧 **TROUBLESHOOTING**

### Lỗi thường gặp:
1. **Port đã được sử dụng:** Thay đổi port trong `launchSettings.json`
2. **Database lỗi:** Chạy `dotnet ef database update`
3. **Không đăng nhập được:** Kiểm tra chính tả email/password

### Reset dữ liệu:
```bash
# Xóa database và tạo lại
rm JobHub.db
dotnet ef database update
```

---

## 📞 **LIÊN HỆ HỖ TRỢ**
- Email: support@jobhub.vn
- Phone: 1900 1234
- Website: https://jobhub.vn

---

*Cập nhật lần cuối: $(Get-Date -Format "dd/MM/yyyy HH:mm")*