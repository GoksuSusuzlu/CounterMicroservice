using FluentValidation;

namespace Application.Features.Counters.Commands.Update;

public class UpdateCounterCommandValidator : AbstractValidator<UpdateCounterCommand>
{
    public UpdateCounterCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.SerialNumber).NotEmpty();
        RuleFor(c => c.MeasurementTime).NotEmpty();
        RuleFor(c => c.LatestIndexInfo).NotEmpty();
        RuleFor(c => c.VoltageDuringMeasurement).NotEmpty();
        RuleFor(c => c.CurrentDuringMeasurement).NotEmpty();
    }
}