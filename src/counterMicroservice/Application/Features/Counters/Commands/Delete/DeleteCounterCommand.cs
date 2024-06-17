using Application.Features.Counters.Constants;
using Application.Features.Counters.Constants;
using Application.Features.Counters.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Counters.Constants.CountersOperationClaims;

namespace Application.Features.Counters.Commands.Delete;

public class DeleteCounterCommand : IRequest<DeletedCounterResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, CountersOperationClaims.Delete];

    public class DeleteCounterCommandHandler : IRequestHandler<DeleteCounterCommand, DeletedCounterResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICounterRepository _counterRepository;
        private readonly CounterBusinessRules _counterBusinessRules;

        public DeleteCounterCommandHandler(IMapper mapper, ICounterRepository counterRepository,
                                         CounterBusinessRules counterBusinessRules)
        {
            _mapper = mapper;
            _counterRepository = counterRepository;
            _counterBusinessRules = counterBusinessRules;
        }

        public async Task<DeletedCounterResponse> Handle(DeleteCounterCommand request, CancellationToken cancellationToken)
        {
            Counter? counter = await _counterRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _counterBusinessRules.CounterShouldExistWhenSelected(counter);

            await _counterRepository.DeleteAsync(counter!);

            DeletedCounterResponse response = _mapper.Map<DeletedCounterResponse>(counter);
            return response;
        }
    }
}