using FluentValidation;
using TodoApi.Application.DTOs;
using FluentValidation;
using TodoApi.Application.DTOs;

namespace TodoApi.Application.Validators;

public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Description).MaximumLength(250).WithMessage("Description cannot exceed 250 characters");
        

    }
    
}