using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CounterRepository : EfRepositoryBase<Counter, Guid, BaseDbContext>, ICounterRepository
{
    public CounterRepository(BaseDbContext context) : base(context)
    {
    }
}