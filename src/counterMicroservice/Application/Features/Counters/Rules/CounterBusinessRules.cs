using Application.Features.Counters.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Application.Features.Counters.Rules;

public class CounterBusinessRules : BaseBusinessRules
{
    private readonly ICounterRepository _counterRepository;
    private readonly ILocalizationService _localizationService;

    public CounterBusinessRules(ICounterRepository counterRepository, ILocalizationService localizationService)
    {
        _counterRepository = counterRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CountersBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CounterShouldExistWhenSelected(Counter? counter)
    {
        if (counter == null)
            await throwBusinessException(CountersBusinessMessages.CounterNotExists);
    }

    public async Task CounterIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Counter? counter = await _counterRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CounterShouldExistWhenSelected(counter);
    }

    public async Task SerialNumberCannotBeDuplicatedWhenInserted(string serialNumber)
    {
        Counter? result = await _counterRepository.GetAsync(predicate: c => c.SerialNumber.ToLower() == serialNumber.ToLower());

        if (result != null)
        {
            throw new BusinessException(CountersBusinessMessages.SerialNumberExists);
        }
    }
}