# Student Management Console v2

## 1. Giới thiệu

Student Management Console là ứng dụng quản lý sinh viên được xây dựng bằng C# Console App.

Project được thực hiện nhằm thực hành kiến thức C# cơ bản, lập trình hướng đối tượng (OOP), Interface, LINQ và lập trình bất đồng bộ (async/await).

Dữ liệu sinh viên hiện được lưu trong List<Student> trong bộ nhớ, chưa sử dụng Database.

## 2. Các chức năng

Ứng dụng có các chức năng chính:

1. Thêm sinh viên.
2. Xem danh sách sinh viên.
3. Tìm kiếm sinh viên theo ID.
4. Cập nhật thông tin sinh viên.
5. Xóa sinh viên.
6. Thống kê số lượng sinh viên theo lớp.
7. Hiển thị sinh viên có điểm cao.
8. Thống kê điểm trung bình theo lớp.
9. Tải danh sách sinh viên bất đồng bộ, có hỗ trợ CancellationToken.
0. Thoát chương trình.

## 3. Kiến thức áp dụng

### OOP
- Class và Object.
- Encapsulation.
- Interface.
- Tách StudentService và IStudentService.
- Phân chia trách nhiệm giữa Program và Service.

### LINQ
- Lambda Expression.
- Where, Select, OrderBy, OrderByDescending.
- GroupBy, Count, Average.
- Lọc, sắp xếp và thống kê dữ liệu sinh viên.

### Async/Await
- Task và Task<T>.
- async/await.
- Task.Delay để mô phỏng thời gian tải dữ liệu.
- CancellationToken và CancellationTokenSource.
- Xử lý OperationCanceledException khi hủy tác vụ.

## 4. Cấu trúc chương trình

- Program.cs: Hiển thị menu, nhận dữ liệu từ người dùng và gọi các chức năng.
- Student.cs: Định nghĩa thông tin sinh viên.
- IStudentService.cs: Khai báo các chức năng quản lý sinh viên.
- StudentService.cs: Triển khai CRUD, thống kê LINQ và tải dữ liệu bất đồng bộ.

Tên file và vị trí thư mục có thể được điều chỉnh theo cấu trúc source code thực tế.

## 5. Hướng dẫn chạy

Yêu cầu: Máy tính đã cài đặt .NET SDK tương thích với project.

Mở Terminal trong thư mục chứa file Day04.csproj và chạy:

```bash
dotnet restore
dotnet build
dotnet run
```

Sau khi chương trình chạy, nhập số tương ứng để lựa chọn chức năng trong menu.

## 6. Kiểm thử

Các trường hợp đã thực hành kiểm thử:

- Thêm sinh viên mới.
- Không cho phép thêm sinh viên trùng ID.
- Tìm sinh viên có ID không tồn tại.
- Thống kê sinh viên theo lớp và điểm.
- Kiểm tra thống kê với danh sách rỗng.
- Tải dữ liệu async thành công.
- Hủy tải dữ liệu bằng CancellationToken.

## 7. Hạn chế hiện tại

- Dữ liệu được lưu trong bộ nhớ, sẽ mất khi chương trình kết thúc.
- Chưa kết nối SQL Server.
- Task.Delay chỉ mô phỏng độ trễ tải dữ liệu, chưa phải truy vấn Database thực tế.

## 8. Định hướng phát triển

Tiếp tục phát triển Student Management System thành ASP.NET Core Web API, kết nối SQL Server và Entity Framework Core, bổ sung validation, authentication và các chức năng Backend theo lộ trình thực tập.