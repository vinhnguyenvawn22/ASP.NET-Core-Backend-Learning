# Student Management System — C# Console CRUD

## Giới thiệu
Ứng dụng quản lý sinh viên bằng C# Console (.NET 8), sử dụng `List<Student>` để lưu trữ dữ liệu trong bộ nhớ.

## Chức năng
- **Create:** Thêm sinh viên, không cho phép trùng ID.
- **Read:** Hiển thị danh sách sinh viên.
- **Search:** Tìm sinh viên theo ID.
- **Update:** Cập nhật tên và tuổi sinh viên.
- **Delete:** Xóa sinh viên theo ID.
- **Validation:** Kiểm tra ID, tên, tuổi và lựa chọn menu.

## Công nghệ
- C# / .NET 8
- `List<Student>`
- Methods, `while`, `switch`, `foreach`
- Git

## Cách chạy
Yêu cầu: Đã cài .NET 8 SDK.

```bash
cd Week_01/Ngay5_CURD
dotnet run
```

## Menu
```text
1. Them sinh vien
2. Xem danh sach
3. Tim sinh vien
4. Cap nhat sinh vien
5. Xoa sinh vien
0. Thoat
```

## Dữ liệu mẫu
| ID | Name | Age |
|---|---|---|
| SV001 | An | 20 |
| SV002 | Binh | 21 |
| SV003 | Minh | 22 |
