using FluentValidation;

namespace Application.Features.Counters.Commands.Delete;

public class DeleteCounterCommandValidator : AbstractValidator<DeleteCounterCommand>
{
    public DeleteCounterCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}