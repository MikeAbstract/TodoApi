using FluentValidation;
using TodoApi.Application.DTOs;

namespace TodoApi.Application.Validators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .MinimumLength(3)
            .MaximumLength(30);
        
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .Must(email =>
                email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase) ||
                email.EndsWith("@hotmail.com", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Only Gmail or Hotmail emails are allowed");
        
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(14)
            .Matches("[A-Z]").WithMessage("Must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Must contain at least one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Must contain at least one special character");

    }
    
}

