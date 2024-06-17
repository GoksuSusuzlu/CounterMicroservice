using Application.Features.Counters.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Counters;

public class CounterManager : ICounterService
{
    private readonly ICounterRepository _counterRepository;
    private readonly CounterBusinessRules _counterBusinessRules;

    public CounterManager(ICounterRepository counterRepository, CounterBusinessRules counterBusinessRules)
    {
        _counterRepository = counterRepository;
        _counterBusinessRules = counterBusinessRules;
    }

    public async Task<Counter?> GetAsync(
        Expression<Func<Counter, bool>> predicate,
        Func<IQueryable<Counter>, IIncludableQueryable<Counter, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Counter? counter = await _counterRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return counter;
    }

    public async Task<IPaginate<Counter>?> GetListAsync(
        Expression<Func<Counter, bool>>? predicate = null,
        Func<IQueryable<Counter>, IOrderedQueryable<Counter>>? orderBy = null,
        Func<IQueryable<Counter>, IIncludableQueryable<Counter, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Counter> counterList = await _counterRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return counterList;
    }

    public async Task<Counter> AddAsync(Counter counter)
    {
        Counter addedCounter = await _counterRepository.AddAsync(counter);

        return addedCounter;
    }

    public async Task<Counter> UpdateAsync(Counter counter)
    {
        Counter updatedCounter = await _counterRepository.UpdateAsync(counter);

        return updatedCounter;
    }

    public async Task<Counter> DeleteAsync(Counter counter, bool permanent = false)
    {
        Counter deletedCounter = await _counterRepository.DeleteAsync(counter);

        return deletedCounter;
    }
}
