using FluentValidation;
using TimeScout.Application.DTOs.User;

namespace TimeScout.Application.Validator.User;

public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequestDto>
{
    public UserUpdateRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(6).WithMessage("Username must be at least 6 characters");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MinimumLength(1).WithMessage("First name must be at least 1 character")
            .MaximumLength(100).WithMessage("First name must not over than 100 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MinimumLength(1).WithMessage("Last name must be at least 1 character")
            .MaximumLength(100).WithMessage("Last name must not over than 100 characters");
    }
}
