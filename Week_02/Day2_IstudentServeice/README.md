# Student Management Console — OOP & Interface

## 1. Giới thiệu

Đây là chương trình quản lý sinh viên được xây dựng bằng **C# Console Application**.

Project thuộc bài thực hành **Tuần 2 — Ngày 2** trong lộ trình thực tập ASP.NET Core Backend Developer.

Mục tiêu là áp dụng các kiến thức OOP để xây dựng hệ thống quản lý sinh viên, đặc biệt là:

- Class và Object
- Encapsulation
- Abstraction
- Interface
- Polymorphism
- Separation of Concerns

## 2. Chức năng

| Chức năng | Mô tả |
|---|---|
| Add Student | Thêm sinh viên mới, kiểm tra ID trùng |
| View Students | Hiển thị danh sách sinh viên |
| Search Student | Tìm sinh viên theo ID |
| Update Student | Cập nhật tên và tuổi |
| Delete Student | Xóa sinh viên theo ID |
| Exit | Thoát chương trình |

## 3. Cấu trúc project

```text
Day2_IStudentService/
├── Program.cs
├── students.cs
├── IStudentService.cs
├── StudentService.cs
├── Day2_IStudentService.csproj
└── README.md
```

### Program.cs

- Hiển thị menu.
- Nhận dữ liệu từ người dùng.
- Kiểm tra dữ liệu nhập cơ bản.
- Gọi các method thông qua `IStudentService`.
- Hiển thị kết quả.

### students.cs

Chứa class `Student` với các thuộc tính:

- `Id`: Mã sinh viên.
- `Name`: Tên sinh viên.
- `Age`: Tuổi sinh viên.

### IStudentService.cs

Định nghĩa hợp đồng quản lý sinh viên gồm các method:

- `AddStudent()`
- `GetStudents()`
- `GetStudentById()`
- `Updatestudent()`
- `Deletestudent()`

### StudentService.cs

Triển khai `IStudentService` và xử lý nghiệp vụ CRUD trên `List<Student>`.

## 4. Kiến trúc chương trình

```text
Program.cs
    |
    v
IStudentService
    |
    v
StudentService
    |
    v
List<Student>
```

Chương trình sử dụng interface:

```csharp
Istudentservice service = new StudentService();
```

Điều này giúp `Program.cs` gọi các chức năng thông qua hợp đồng mà không cần biết chi tiết triển khai bên trong `StudentService`.

## 5. Cách chạy chương trình

### Yêu cầu

- .NET SDK tương thích với phiên bản được khai báo trong file `.csproj`.
- Visual Studio Code, Visual Studio hoặc Terminal.

### Các bước chạy

Mở Terminal tại thư mục project:

```bash
cd Week_02/Day2_IStudentService
```

Build project:

```bash
dotnet build
```

Chạy chương trình:

```bash
dotnet run
```

### Menu chương trình

```text
========== MENU ==========
1. Them sinh vien
2. Xem danh sach
3. Tim sinh vien
4. Cap nhat sinh vien
5. Xoa sinh vien
0. Thoat
```

## 6. Kiểm thử chức năng

Các trường hợp cần kiểm thử:

| Thao tác | Kết quả mong đợi |
|---|---|
| Thêm sinh viên có ID mới | Thêm thành công |
| Thêm sinh viên có ID trùng | Thêm thất bại |
| Xem danh sách | Hiển thị sinh viên hiện có |
| Tìm sinh viên có ID tồn tại | Trả về thông tin sinh viên |
| Cập nhật sinh viên có ID tồn tại | Thay đổi Name và Age |
| Xóa sinh viên có ID tồn tại | Xóa thành công |
| Xóa sinh viên không tồn tại | Xóa thất bại |

## 7. Kiến thức áp dụng

**Interface:** `IStudentService` quy định các method mà `StudentService` phải triển khai.

**Abstraction:** `Program.cs` sử dụng các chức năng được công khai mà không cần biết thuật toán CRUD bên trong Service.

**Polymorphism:** Biến kiểu `IStudentService` có thể tham chiếu đến object `StudentService` và gọi implementation tương ứng.

**Encapsulation:** Danh sách sinh viên được khai báo private trong `StudentService`.

**Separation of Concerns:** `Program.cs` chịu trách nhiệm giao tiếp với người dùng, còn `StudentService` chịu trách nhiệm quản lý sinh viên.
