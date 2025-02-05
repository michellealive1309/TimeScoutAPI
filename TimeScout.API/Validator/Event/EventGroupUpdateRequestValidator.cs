using FluentValidation;
using TimeScout.API.DTOs.EventGroup;

namespace TimeScout.API.Validator.Event;

public class EventGroupUpdateRequestValidator : AbstractValidator<EventGroupUpdateRequestDto>
{
    public EventGroupUpdateRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id should not be empty.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name should not be empty.")
            .MaximumLength(200).WithMessage("Name should not over than 200 characters.");
            
        RuleFor(x => x.Members)
            .NotNull().WithMessage("Members should not be null.")
            .ForEach(member => member.SetValidator(new MemberRequestValidator()));
    }
}
