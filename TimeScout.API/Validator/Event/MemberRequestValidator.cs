using System;
using FluentValidation;
using TimeScout.API.DTOs.EventGroup;

namespace TimeScout.API.Validator.Event;

public class MemberRequestValidator : AbstractValidator<MemberRequestDto>
{
    public MemberRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id should not be empty.");
    }
}
