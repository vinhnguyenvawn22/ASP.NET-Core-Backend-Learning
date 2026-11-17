using Microsoft.AspNetCore.Mvc;

namespace StudentManagement.Api.Models;

using StudentManagement.Api.Dtos;
using StudentManagement.Api.Responses;
using StudentManagement.Api.Validators;
[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private static readonly List<Student> _students = new List<Student>
    {
        new Student
        {
            Id = 1,
            Name = "Nguyen Van An",
            Age = 20,
        },
        new Student
        {
            Id = 2,
            Name = "Tran Thi Binh",
            Age = 21,
        },
    };

    [HttpGet]
    public IActionResult GetAllStudents(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] string? name = null
    )
    {
        if (pageNumber < 1)
        {
            return BadRequest("pageNumber must be greater than or equal to 1");
        }

        if (pageSize < 1 || pageSize > 100)
        {
            return BadRequest(
                "pageSize must be greater than or equal to 1 and smaller than or equal to 100"
            );
        }
        IEnumerable<Student> query = _students;
        if (!string.IsNullOrWhiteSpace(name))
        {
            
            query = query.Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
        var responses = query
            .OrderBy(s => s.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new StudentResponseDto
            {
                Id = s.Id,
                Age = s.Age,
                Name = s.Name,
            })
            .ToList();
        var result = new ApiResponse<List<StudentResponseDto>>
        {
            Success = true,
            Message = "Students retrieved successfully",
            Data = responses,
        };
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentById(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        var response = new StudentResponseDto
        {
            Id = student.Id,
            Age = student.Age,
            Name = student.Name,
        };
        var result = new ApiResponse<StudentResponseDto>
        {
            Success = true,
            Message = "Student retrieved successfully",
            Data = response,
        };
        return Ok(result);
    }

    [HttpPost]
    public IActionResult CreateStudent([FromBody] CreateStudentDto dto)
    {
        var validator = new CreateStudentDtoValidator();
        var result = validator.Validate(dto);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage
            }).ToList();
            return BadRequest(errors);
        }
        int newID = _students.Count == 0 ? 1 : _students.Max(s => s.Id) + 1;
        var student = new Student
        {
            Id = newID,
            Name = dto.Name,
            Age = dto.Age,
        };
        _students.Add(student);
        var response = new StudentResponseDto
        {
            Id = student.Id,
            Age = student.Age,
            Name = student.Name,
        };
        return CreatedAtAction(nameof(GetStudentById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] UpdateStudentDto dto)
    {
        var validator = new UpdateStudentDtoValidator();
        var result = validator.Validate(dto);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage
            }).ToList();
            return BadRequest(errors);
        }
        var existingStudent = _students.FirstOrDefault(s => s.Id == id);
        if (existingStudent is null)
        {
            return NotFound();
        }
        existingStudent.Name = dto.Name;
        existingStudent.Age = dto.Age;
        var response = new StudentResponseDto
        {
            Id = existingStudent.Id,
            Name = existingStudent.Name,
            Age = existingStudent.Age,
        };
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var student = _students.FirstOrDefault(s => s.Id == id);
        if (student is null)
        {
            return NotFound();
        }
        _students.Remove(student);
        return NoContent();
    }

    [HttpGet("search")]
    public IActionResult SearchStudents([FromQuery] string name)
    {
        var students = _students
            .Where(s => s.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(students);
    }
}
