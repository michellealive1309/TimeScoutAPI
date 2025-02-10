using FluentValidation;
using TimeScout.Application.DTOs.Authentication;

namespace TimeScout.Application.Validator.Register;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Username)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(24);

        RuleFor(x => x.FirstName)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(x => x.LastName)
            .MaximumLength(100)
            .NotEmpty();
    }
}
