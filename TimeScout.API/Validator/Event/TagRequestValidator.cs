using FluentValidation;
using TimeScout.API.DTOs.Event;

namespace TimeScout.API.Validator.Event;

public class TagRequestValidator : AbstractValidator<TagRequestDto>
{
    public TagRequestValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id should not be 0.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name should not be empty.");

        RuleFor(x => x.Color).NotEmpty().WithMessage("Color should not be empty.");
        
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId should not be empty.");
    }
}
