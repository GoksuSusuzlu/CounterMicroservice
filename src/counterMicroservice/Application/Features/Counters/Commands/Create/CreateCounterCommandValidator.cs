using FluentValidation;

namespace Application.Features.Counters.Commands.Create;

public class CreateCounterCommandValidator : AbstractValidator<CreateCounterCommand>
{
    public CreateCounterCommandValidator()
    {
        RuleFor(c => c.SerialNumber).NotEmpty();
        RuleFor(c => c.MeasurementTime).NotEmpty();
        RuleFor(c => c.LatestIndexInfo).NotEmpty();
        RuleFor(c => c.VoltageDuringMeasurement).NotEmpty();
        RuleFor(c => c.CurrentDuringMeasurement).NotEmpty();
    }
}