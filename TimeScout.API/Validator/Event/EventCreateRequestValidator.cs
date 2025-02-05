using FluentValidation;
using TimeScout.API.DTOs.Event;

namespace TimeScout.API.Validator.Event;

public class EventCreateRequestValidator : AbstractValidator<EventCreateRequestDto>
{
    public EventCreateRequestValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name should not over than 100 characters.")
            .NotEmpty().WithMessage("Name should not be empty.");

        RuleFor(x => x.Detail).MaximumLength(200).WithMessage("Detail should not over than 200 characters.");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date should not be empty.")
            .Must(sd => DateOnly.TryParseExact(sd, "yyyy-MM-dd", out _))
            .WithMessage($"Start date should be in format {DateTime.UtcNow:yyyy-MM-dd}.");

        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time should not be empty.")
            .Must(st => TimeOnly.TryParseExact(st, "HH:mm", out _))
            .WithMessage($"Start time should be in format {DateTime.UtcNow:HH:mm}.");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date should not be empty.")
            .Must(ed => DateOnly.TryParseExact(ed, "yyyy-MM-dd", out _))
            .WithMessage($"End date should be in format {DateTime.UtcNow:yyyy-MM-dd}.");

        RuleFor(x => x.EndTime)
            .NotNull().WithMessage("End time should not be empty.")
            .Must(et => TimeOnly.TryParseExact(et, "HH:mm", out _))
            .WithMessage($"End time should be in format {DateTime.UtcNow:HH:mm}.");

        RuleFor(x => x).Custom((x, context) => {
            DateOnly.TryParseExact(x.StartDate!, "yyyy-MM-dd", out var parsedStartDate);
            DateOnly.TryParseExact(x.EndDate!, "yyyy-MM-dd", out var parsedEndDate);
            TimeOnly.TryParseExact(x.StartTime!, "HH:mm", out var parsedStartTime);
            TimeOnly.TryParseExact(x.EndTime!, "HH:mm", out var parsedEndTime);

            if (parsedEndDate < parsedStartDate)
            {
                context.AddFailure("End date should not be the date before start date.");
            }

            if (parsedEndTime < parsedStartTime)
            {
                context.AddFailure("End time should not be the time before start time.");
            }
        });

        RuleFor(x => x.IsShared).NotNull().WithMessage("IsShared should not be null.");

        RuleFor(x => x.EventGroupId).GreaterThan(0).WithMessage("EventGroupId should not be 0.");

        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId should not be empty.");
    }
}
