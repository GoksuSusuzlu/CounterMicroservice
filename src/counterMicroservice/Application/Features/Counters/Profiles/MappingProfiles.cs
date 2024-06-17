using Application.Features.Counters.Commands.Create;
using Application.Features.Counters.Commands.Delete;
using Application.Features.Counters.Commands.Update;
using Application.Features.Counters.Queries.GetById;
using Application.Features.Counters.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Counters.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Counter, CreateCounterCommand>().ReverseMap();
        CreateMap<Counter, CreatedCounterResponse>().ReverseMap();
        CreateMap<Counter, UpdateCounterCommand>().ReverseMap();
        CreateMap<Counter, UpdatedCounterResponse>().ReverseMap();
        CreateMap<Counter, DeleteCounterCommand>().ReverseMap();
        CreateMap<Counter, DeletedCounterResponse>().ReverseMap();
        CreateMap<Counter, GetByIdCounterResponse>().ReverseMap();
        CreateMap<Counter, GetListCounterListItemDto>().ReverseMap();
        CreateMap<IPaginate<Counter>, GetListResponse<GetListCounterListItemDto>>().ReverseMap();
    }
}