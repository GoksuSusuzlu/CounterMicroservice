using Application.Features.Counters.Constants;
using Application.Features.Counters.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Counters.Constants.CountersOperationClaims;

namespace Application.Features.Counters.Commands.Update;

public class UpdateCounterCommand : IRequest<UpdatedCounterResponse>, ISecuredRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string SerialNumber { get; set; }
    public DateTime MeasurementTime { get; set; }
    public string LatestIndexInfo { get; set; }
    public double VoltageDuringMeasurement { get; set; }
    public double CurrentDuringMeasurement { get; set; }

    public string[] Roles => [Admin, Write, CountersOperationClaims.Update];

    public class UpdateCounterCommandHandler : IRequestHandler<UpdateCounterCommand, UpdatedCounterResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICounterRepository _counterRepository;
        private readonly CounterBusinessRules _counterBusinessRules;

        public UpdateCounterCommandHandler(IMapper mapper, ICounterRepository counterRepository,
                                         CounterBusinessRules counterBusinessRules)
        {
            _mapper = mapper;
            _counterRepository = counterRepository;
            _counterBusinessRules = counterBusinessRules;
        }

        public async Task<UpdatedCounterResponse> Handle(UpdateCounterCommand request, CancellationToken cancellationToken)
        {
            Counter? counter = await _counterRepository.GetAsync(predicate: c => c.Id == request.Id, cancellationToken: cancellationToken);
            await _counterBusinessRules.CounterShouldExistWhenSelected(counter);
            counter = _mapper.Map(request, counter);

            await _counterRepository.UpdateAsync(counter!);

            UpdatedCounterResponse response = _mapper.Map<UpdatedCounterResponse>(counter);
            return response;
        }
    }
}