using FluentValidation;
using TimeScout.API.DTOs.EventGroup;

namespace TimeScout.API.Validator.Event;

public class EventGroupUpdateRequestValidator : AbstractValidator<EventGroupUpdateRequestDto>
{
    public EventGroupUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name should not be empty.")
            .MaximumLength(200).WithMessage("Name should not over than 200 characters.");
    }
}
