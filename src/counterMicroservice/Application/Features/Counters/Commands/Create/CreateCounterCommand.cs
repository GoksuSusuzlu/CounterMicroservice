using Application.Features.Counters.Constants;
using Application.Features.Counters.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Counters.Constants.CountersOperationClaims;

namespace Application.Features.Counters.Commands.Create;

public class CreateCounterCommand : IRequest<CreatedCounterResponse>, ISecuredRequest, ITransactionalRequest
{
    public string SerialNumber { get; set; }
    public DateTime MeasurementTime { get; set; }
    public string LatestIndexInfo { get; set; }
    public double VoltageDuringMeasurement { get; set; }
    public double CurrentDuringMeasurement { get; set; }

    public string[] Roles => [Admin, Write, CountersOperationClaims.Create];

    public class CreateCounterCommandHandler : IRequestHandler<CreateCounterCommand, CreatedCounterResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICounterRepository _counterRepository;
        private readonly CounterBusinessRules _counterBusinessRules;

        public CreateCounterCommandHandler(IMapper mapper, ICounterRepository counterRepository,
                                         CounterBusinessRules counterBusinessRules)
        {
            _mapper = mapper;
            _counterRepository = counterRepository;
            _counterBusinessRules = counterBusinessRules;
        }

        public async Task<CreatedCounterResponse> Handle(CreateCounterCommand request, CancellationToken cancellationToken)
        {
            await _counterBusinessRules.SerialNumberCannotBeDuplicatedWhenInserted(request.SerialNumber);

            Counter counter = _mapper.Map<Counter>(request);

            await _counterRepository.AddAsync(counter);

            CreatedCounterResponse response = _mapper.Map<CreatedCounterResponse>(counter);
            return response;
        }
    }
}