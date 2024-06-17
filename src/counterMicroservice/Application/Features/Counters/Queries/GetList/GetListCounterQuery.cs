using Application.Features.Counters.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Counters.Constants.CountersOperationClaims;

namespace Application.Features.Counters.Queries.GetList;

public class GetListCounterQuery : IRequest<GetListResponse<GetListCounterListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetListCounterQueryHandler : IRequestHandler<GetListCounterQuery, GetListResponse<GetListCounterListItemDto>>
    {
        private readonly ICounterRepository _counterRepository;
        private readonly IMapper _mapper;

        public GetListCounterQueryHandler(ICounterRepository counterRepository, IMapper mapper)
        {
            _counterRepository = counterRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListCounterListItemDto>> Handle(GetListCounterQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Counter> counters = await _counterRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListCounterListItemDto> response = _mapper.Map<GetListResponse<GetListCounterListItemDto>>(counters);
            return response;
        }
    }
}