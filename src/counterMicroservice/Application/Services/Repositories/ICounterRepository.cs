using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICounterRepository : IAsyncRepository<Counter, Guid>, IRepository<Counter, Guid>
{
}