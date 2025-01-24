using System;
using FluentValidation;
using TimeScout.API.DTOs.EventGroup;

namespace TimeScout.API.Validator.Event;

public class EventGroupCreateRequestValidator : AbstractValidator<EventGroupCreateRequestDto>
{
    public EventGroupCreateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name should not be empty.")
            .MaximumLength(200).WithMessage("Name should not over than 200 characters.");
    }
}
