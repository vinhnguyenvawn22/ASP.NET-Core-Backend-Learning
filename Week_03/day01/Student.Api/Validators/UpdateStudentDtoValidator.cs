namespace StudentManagement.Api.Validators;
using FluentValidation;
using StudentManagement.Api.Dtos;

public class UpdateStudentDtoValidator : AbstractValidator<UpdateStudentDto>
{
    public UpdateStudentDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Student name is required");

        RuleFor(x => x.Age).GreaterThan(0).WithMessage("Student age must be greater than 0");
    }
}