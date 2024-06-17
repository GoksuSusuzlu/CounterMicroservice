using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Counters;

public interface ICounterService
{
    Task<Counter?> GetAsync(
        Expression<Func<Counter, bool>> predicate,
        Func<IQueryable<Counter>, IIncludableQueryable<Counter, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Counter>?> GetListAsync(
        Expression<Func<Counter, bool>>? predicate = null,
        Func<IQueryable<Counter>, IOrderedQueryable<Counter>>? orderBy = null,
        Func<IQueryable<Counter>, IIncludableQueryable<Counter, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Counter> AddAsync(Counter counter);
    Task<Counter> UpdateAsync(Counter counter);
    Task<Counter> DeleteAsync(Counter counter, bool permanent = false);
}
