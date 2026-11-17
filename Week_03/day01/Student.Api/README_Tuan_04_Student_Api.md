# Tuần 4 — Middleware, Validation, Exception Handling & Logging

## Tổng quan

Tuần 4 của project **Student Management API** tập trung vào việc quản lý HTTP Request Pipeline, bắt exception tập trung, kiểm tra dữ liệu đầu vào và ghi log có cấu trúc. Các tính năng được triển khai trên **ASP.NET Core Web API (.NET 8)**, kế thừa API CRUD sinh viên từ các tuần trước.

**Trạng thái:** Hoàn thành Tuần 4 — PASS tất cả checkpoint và 7/7 bước kiểm tra cuối tuần.

## Công nghệ

- C# / .NET 8, ASP.NET Core Web API (Controllers)
- FluentValidation **12.2.0**
- `ILogger<T>` và logging tích hợp của ASP.NET Core
- `Stopwatch` (`System.Diagnostics`)
- Swagger / OpenAPI
- LINQ; dữ liệu vẫn lưu trong `List<Student>` (in-memory), **chưa kết nối database**

## Cấu trúc file liên quan

```text
Student.Api/
├── Controllers/
│   └── StudentsController.cs
├── Dtos/
│   ├── CreateStudentDto.cs
│   └── UpdateStudentDto.cs
├── Middlewares/
│   └── GlobalExceptionMiddleware.cs
├── Validators/
│   ├── CreateStudentDtoValidator.cs
│   └── UpdateStudentDtoValidator.cs
├── Responses/
│   └── ApiResponse.cs
├── Program.cs
├── appsettings.json
└── appsettings.Development.json
```

## 1. Request Logging Middleware

Middleware ghi **HTTP Method**, **Request Path**, **Duration** (mili giây) và **Status Code**. Request và Duration được ghi trước/sau `await next()`; dùng `try/finally` để luôn ghi Duration khi các thành phần phía sau ném exception.

Ví dụ log cho request thành công:

```text
info: Student.Api[0]
      Request: GET /api/students/2
info: Student.Api[0]
      Request Duration: 113 ms
info: Student.Api[0]
      Status Code: 200
```

> Nếu exception chưa xử lý truyền ra khỏi Request Logging Middleware, dòng log Status Code nằm sau `finally` sẽ không chạy. Duration vẫn được ghi. Giá trị thời gian trên chỉ là kết quả của một lần kiểm thử, không phải benchmark.

## 2. Global Exception Middleware

`GlobalExceptionMiddleware` là class-based middleware, được đăng ký thông qua `app.UseMiddleware<GlobalExceptionMiddleware>()`. Nó sử dụng `RequestDelegate`, `InvokeAsync(HttpContext)` và constructor injection cho `ILogger<GlobalExceptionMiddleware>`.

Khi nhận exception chưa được xử lý từ các thành phần phía sau, middleware:

1. Ghi lỗi bằng `_logger.LogError(ex, "An unexpected error occurred")`, bao gồm thông tin exception phục vụ debug trên server.
2. Thiết lập HTTP **500 Internal Server Error**.
3. Trả về JSON theo `ApiResponse<object>` mà không tiết lộ `ex.Message` hay stack trace cho client.

Ví dụ response lỗi `500`:

```json
{
  "success": false,
  "message": "An unexpected error occurred",
  "data": null
}
```

Middleware được đặt **trước Request Logging Middleware** trong `Program.cs` để bắt exception truyền ra từ các thành phần phía sau. Endpoint `/test-error` chỉ dùng để thử nghiệm đã được xóa sau khi hoàn tất kiểm thử.

> Giới hạn hiện tại: Chưa xử lý riêng trường hợp `HttpResponse.HasStarted` (response đã bắt đầu được gửi).

## 3. Validation với FluentValidation

Đã tạo hai validator:

- `CreateStudentDtoValidator`: kiểm tra request **POST**.
- `UpdateStudentDtoValidator`: kiểm tra request **PUT**.

Các quy tắc hiện tại:

| Thuộc tính | Quy tắc | Thông báo lỗi |
|---|---|---|
| `Name` | `NotEmpty()` | `Student name is required` |
| `Age` | `GreaterThan(0)` | `Student age must be greater than 0` |

Validation được gọi thủ công qua `validator.Validate(dto)` trong Controller. Nếu không hợp lệ, API trả HTTP **400 Bad Request** trước khi tạo/cập nhật sinh viên. Response lỗi được rút gọn bằng LINQ `Select()` để chỉ giữ `PropertyName` và `ErrorMessage`.

**Request mẫu không hợp lệ:**

```http
POST /api/students
Content-Type: application/json
```

```json
{
  "name": "",
  "age": -5
}
```

**Response — 400 Bad Request:**

```json
[
  {
    "propertyName": "Name",
    "errorMessage": "Student name is required"
  },
  {
    "propertyName": "Age",
    "errorMessage": "Student age must be greater than 0"
  }
]
```

Với PUT, validation đang chạy **trước khi tìm sinh viên theo ID**. Do đó nếu cả ID không tồn tại và body không hợp lệ, API sẽ trả `400` trước khi kiểm tra `404`.

## 4. ILogger và cấu hình Logging

Request Logging Middleware sử dụng `app.Logger.LogInformation()` với các named placeholders: `{Method}`, `{Path}`, `{DurationMs}`, `{StatusCode}`. Exception Middleware sử dụng `ILogger<GlobalExceptionMiddleware>.LogError(ex, ...)`.

Các mức log đã tìm hiểu: `Trace`, `Debug`, `Information`, `Warning`, `Error`, `Critical`.

Cấu hình đã thực hành trong `appsettings.Development.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

Đã xác nhận khi đổi `Default` thành `Warning`, log `Information` bị lọc; khi chuyển về `Information`, log Request xuất hiện trở lại. `LogError` vẫn được phép ghi trong cả hai trường hợp.

## 5. Chạy ứng dụng và kiểm thử

Tại thư mục chứa `Student.Api.csproj`:

```bash
dotnet restore
dotnet build
dotnet run
```

Mở Swagger theo địa chỉ được ứng dụng cung cấp, ví dụ `http://localhost:5260/swagger` trong môi trường Development.

| Kiểm thử | Kết quả mong đợi | Trạng thái |
|---|---|---|
| `dotnet build` | Build thành công | PASS |
| `GET /api/students/2` | `200 OK` | PASS |
| `GET /api/students/999` | `404 Not Found` | PASS |
| POST với `Name=""`, `Age=-5` | `400` với 2 lỗi rút gọn | PASS |
| PUT với `Name=""`, `Age=-10` | `400` với 2 lỗi rút gọn | PASS |
| Exception thử nghiệm | `500` + JSON lỗi chung + log `Error` | PASS (đã xóa endpoint thử nghiệm) |
| Đăng ký hai Middleware | Đúng thứ tự | PASS |

> Có thể xuất hiện warning `Failed to determine the https port for redirect` khi chạy HTTP mà chưa có cấu hình cổng HTTPS tương ứng. Warning này đã được quan sát trong lúc thử nghiệm và chưa được xử lý trong Tuần 4.

## 6. Kết quả học tập và hướng phát triển

**Đã hoàn thành:**

- Hiểu Request Pipeline, `app.Use()`, `await next()` và thứ tự Middleware.
- Tạo `GlobalExceptionMiddleware`, xử lý HTTP 500 và trả JSON an toàn.
- Áp dụng `try/catch/finally`, `Stopwatch` và Structured Logging.
- Tạo Validator cho POST/PUT, ngăn dữ liệu không hợp lệ bằng HTTP 400.
- Kiểm thử CRUD liên quan bằng Swagger; hoàn thành checkpoint Tuần 4.

**Cải tiến sau này:** Dependency Injection cho validators, xử lý `Response.HasStarted`, chuẩn hóa response toàn API, cấu hình HTTPS và tăng khả năng quan sát log.

**Tuần tiếp theo:** Tuần 5 — SQL Server; học nền tảng cơ sở dữ liệu để chuẩn bị thay thế lưu trữ in-memory.
