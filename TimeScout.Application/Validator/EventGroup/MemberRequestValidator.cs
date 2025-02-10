using System;
using FluentValidation;
using TimeScout.Application.DTOs.EventGroup;

namespace TimeScout.Application.Validator.EventGroup;

public class MemberRequestValidator : AbstractValidator<MemberRequestDto>
{
    public MemberRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id should not be empty.");
    }
}
