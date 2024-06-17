using Application.Features.Counters.Constants;
using Application.Features.Counters.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Counters.Constants.CountersOperationClaims;

namespace Application.Features.Counters.Queries.GetById;

public class GetByIdCounterQuery : IRequest<GetByIdCounterResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdCounterQueryHandler : IRequestHandler<GetByIdCounterQuery, GetByIdCounterResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICounterRepository _counterRepository;
        private readonly CounterBusinessRules _counterBusinessRules;

        public GetByIdCounterQueryHandler(IMapper mapper, ICounterRepository counterRepository, CounterBusinessRules counterBusinessRules)
        {
            _mapper = mapper;
            _counterRepository = counterRepository;
            _counterBusinessRules = counterBusinessRules;
        }

        public async Task<GetByIdCounterResponse> Handle(GetByIdCounterQuery request, CancellationToken cancellationToken)
        {
            Counter? counter = await _counterRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _counterBusinessRules.CounterShouldExistWhenSelected(counter);

            GetByIdCounterResponse response = _mapper.Map<GetByIdCounterResponse>(counter);
            return response;
        }
    }
}