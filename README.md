<img width="1535" height="717" alt="image" src="https://github.com/user-attachments/assets/131e6a84-71b0-4e19-bdc1-eb20bc6ccb05" />
<img width="1530" height="727" alt="image" src="https://github.com/user-attachments/assets/c6bfbe28-b34b-4c4d-b6b1-e5a91a985b55" />



# Student Management API

RESTful Web API quản lý sinh viên được xây dựng bằng **ASP.NET Core Web API (.NET 8)**. Đây là project thực hành backend, tập trung vào Controller, Routing, Model Binding, DTO, HTTP Status Code, LINQ, tìm kiếm, phân trang và cấu trúc response thống nhất.

## Công nghệ sử dụng

- C# / .NET 8
- ASP.NET Core Web API (Controllers)
- LINQ
- Swagger / OpenAPI để thử nghiệm API
- Dữ liệu in-memory với `List<Student>` (chưa sử dụng database)

## Chạy ứng dụng

**Yêu cầu:** Đã cài .NET 8 SDK.

Mở terminal tại thư mục chứa `Student.Api.csproj` rồi chạy:

```bash
dotnet restore
dotnet build
dotnet run
```

Mở địa chỉ Swagger được cấu hình cho ứng dụng, ví dụ:

```text
http://localhost:5260/swagger
```

> Port có thể khác tùy cấu hình máy và launch profile. Nếu Swagger chỉ được bật trong môi trường Development, hãy chạy ứng dụng với môi trường Development.

## Cấu trúc thư mục chính

```text
Student.Api/
├── Controllers/
│   └── StudentsController.cs
├── Models/
│   └── Student.cs
├── Dtos/
│   ├── CreateStudentDto.cs
│   ├── UpdateStudentDto.cs
│   └── StudentResponseDto.cs
├── Responses/
│   └── ApiResponse.cs
├── Program.cs
└── appsettings.json
```

## Chức năng

- Xem danh sách sinh viên, sắp xếp theo ID tăng dần
- Xem sinh viên theo ID
- Tạo sinh viên mới; server tự sinh ID
- Cập nhật tên và tuổi sinh viên
- Xóa sinh viên
- Tìm kiếm theo tên, không phân biệt chữ hoa/chữ thường
- Phân trang và kiểm tra tham số phân trang
- Sử dụng request/response DTO để tách dữ liệu HTTP khỏi model
- Chuẩn hóa response thành công cho GET theo ID và GET danh sách bằng `ApiResponse<T>`

## Danh sách API

| Method | Endpoint | Mô tả | Kết quả chính |
|---|---|---|---|
| `GET` | `/api/students` | Lấy danh sách, hỗ trợ tìm kiếm và phân trang | `200 OK` |
| `GET` | `/api/students/{id}` | Lấy một sinh viên theo ID | `200 OK`, `404 Not Found` |
| `GET` | `/api/students/search?name=An` | Endpoint tìm kiếm theo tên riêng đã thực hành | `200 OK` |
| `POST` | `/api/students` | Tạo sinh viên | `201 Created` |
| `PUT` | `/api/students/{id}` | Cập nhật sinh viên | `200 OK`, `404 Not Found` |
| `DELETE` | `/api/students/{id}` | Xóa sinh viên | `204 No Content`, `404 Not Found` |

### Tham số GET danh sách

| Tham số | Kiểu | Mặc định | Ý nghĩa |
|---|---|---|---|
| `pageNumber` | `int` | `1` | Số trang, phải ≥ 1 |
| `pageSize` | `int` | `5` | Số phần tử mỗi trang, từ 1 đến 100 |
| `name` | `string?` | Không có | Từ khóa tìm kiếm theo tên, không phân biệt hoa/thường |

Ví dụ:

```http
GET /api/students?name=an&pageNumber=1&pageSize=2
```

API lọc theo `name` trước, rồi `OrderBy(Id)`, `Skip`, `Take`, `Select` sang DTO.

### Ví dụ GET danh sách

**Request**

```http
GET /api/students?pageNumber=1&pageSize=2
```

**Response — `200 OK`**

```json
{
  "success": true,
  "message": "Students retrieved successfully",
  "data": [
    { "id": 1, "name": "Nguyen Van An", "age": 20 },
    { "id": 2, "name": "Tran Thi Binh", "age": 21 }
  ]
}
```

Với trang hợp lệ nhưng không có dữ liệu, `data` là mảng rỗng `[]`. Nếu `pageNumber < 1` hoặc `pageSize` nằm ngoài khoảng 1–100, API trả `400 Bad Request`.

### Ví dụ GET theo ID

**Request**

```http
GET /api/students/2
```

**Response — `200 OK`**

```json
{
  "success": true,
  "message": "Student retrieved successfully",
  "data": {
    "id": 2,
    "name": "Tran Thi Binh",
    "age": 21
  }
}
```

Nếu không có sinh viên tương ứng, API trả `404 Not Found`.

### Ví dụ POST tạo sinh viên

**Request**

```http
POST /api/students
Content-Type: application/json
```

```json
{
  "name": "Nguyen Thi Hoa",
  "age": 20
}
```

**Response — `201 Created`**

```http
Location: http://localhost:5260/api/students/3
```

```json
{
  "id": 3,
  "name": "Nguyen Thi Hoa",
  "age": 20
}
```

Client không cần cung cấp `id`. Server sinh ID dựa trên ID lớn nhất hiện có trong danh sách (hoặc bắt đầu bằng 1 nếu danh sách rỗng). URL `Location` và ID minh họa có thể thay đổi.

### Ví dụ PUT cập nhật sinh viên

**Request**

```http
PUT /api/students/2
Content-Type: application/json
```

```json
{
  "name": "Tran Thi Binh Updated",
  "age": 24
}
```

**Response — `200 OK`**

```json
{
  "id": 2,
  "name": "Tran Thi Binh Updated",
  "age": 24
}
```

Sinh viên không tồn tại sẽ nhận `404 Not Found`. ID được xác định bằng route và không được thay đổi qua DTO cập nhật.

### Ví dụ DELETE sinh viên

```http
DELETE /api/students/2
```

- Xóa thành công: `204 No Content` (không có response body).
- ID không tồn tại: `404 Not Found`.

## Model và DTO

| Class | Vai trò | Thuộc tính |
|---|---|---|
| `Student` | Model sinh viên trong hệ thống | `Id`, `Name`, `Age` |
| `CreateStudentDto` | Request tạo sinh viên | `Name`, `Age` |
| `UpdateStudentDto` | Request cập nhật sinh viên | `Name`, `Age` |
| `StudentResponseDto` | Dữ liệu sinh viên trả cho client | `Id`, `Name`, `Age` |
| `ApiResponse<T>` | Wrapper response thành công cho các GET đã triển khai | `Success`, `Message`, `Data` |

> Lưu ý: Hiện tại GET theo ID và GET danh sách trả `ApiResponse<T>`; POST và PUT đang trả trực tiếp `StudentResponseDto`, DELETE trả `204 No Content`. Response lỗi chưa được chuẩn hóa hoàn toàn.

## Kiểm thử thủ công

Các kịch bản đã thực hành với Swagger:

- GET theo ID có tồn tại / không tồn tại.
- POST tạo sinh viên và kiểm tra header `Location` của `201 Created`.
- PUT cập nhật thông tin và GET kiểm tra kết quả.
- DELETE sinh viên, sau đó GET xác nhận `404 Not Found`.
- Tìm kiếm theo tên có kết quả / không có kết quả.
- Phân trang: trang đầu, trang tiếp theo, trang rỗng.
- Validation: `pageNumber=0`, `pageSize=0`, `pageSize=101` trả `400 Bad Request`.

## Giới hạn hiện tại

- Dữ liệu chỉ nằm trong RAM và **mất khi ứng dụng khởi động lại**.
- `List<Student>` dùng chung chưa được thiết kế an toàn cho nhiều request đồng thời.
- Sinh ID bằng `Max(Id) + 1` chỉ phục vụ thực hành, không bảo đảm duy nhất khi có truy cập đồng thời.
- Chưa tích hợp Entity Framework Core hoặc cơ sở dữ liệu.
- Chưa có validation đầy đủ cho `Name` và `Age`, xác thực/phân quyền, automated tests hay metadata tổng số trang.

## Hướng phát triển

- Tách xử lý nghiệp vụ sang Service Layer và sử dụng Dependency Injection.
- Sử dụng Entity Framework Core và SQL Server để lưu trữ lâu dài.
- Bổ sung validation, xử lý exception và chuẩn hóa error response.
- Thêm metadata phân trang (`totalCount`, `totalPages`).
- Viết unit test / integration test, bổ sung authentication và authorization khi học đến các nội dung tương ứng.

---

**Mục đích:** Project thực hành ASP.NET Core Backend — xây dựng Student Management API từ các kiến thức C#, LINQ, HTTP, Controller, DTO, REST và Swagger.
