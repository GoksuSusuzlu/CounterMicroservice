using NArchitecture.Core.Application.Responses;

namespace Application.Features.Counters.Commands.Delete;

public class DeletedCounterResponse : IResponse
{
    public Guid Id { get; set; }
}