using FluentValidation;
using TimeScout.Application.DTOs.EventGroup;

namespace TimeScout.Application.Validator.EventGroup;

public class EventGroupCreateRequestValidator : AbstractValidator<EventGroupCreateRequestDto>
{
    public EventGroupCreateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name should not be empty.")
            .MaximumLength(200).WithMessage("Name should not over than 200 characters.");
            
        RuleFor(x => x.Members)
            .NotNull().WithMessage("Members should not be null.")
            .ForEach(member => member.SetValidator(new MemberRequestValidator()));

    }
}
