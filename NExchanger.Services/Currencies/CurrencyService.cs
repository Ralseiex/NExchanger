using Microsoft.EntityFrameworkCore;
using NExchanger.Persistence;
using NExchanger.Persistence.Entities;

namespace NExchanger.Services.Currencies;

public class CurrencyService : ICurrencyService
{
    private readonly NExchangerContext _dbContext;

    public CurrencyService(NExchangerContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CreateCurrency(string code, string fullName, CancellationToken cancellationToken)
    {
        var currency = await _dbContext.Currencies
            .SingleOrDefaultAsync(currency => currency.Code == code, cancellationToken);
        if (currency is not null) throw new CurrencyAlreadyExistsException();

        var newCurrency = new Currency()
        {
            Code = code,
            Created = DateTime.UtcNow,
            FullName = fullName
        };
        _dbContext.Currencies.Add(newCurrency);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newCurrency.Id;
    }

    public async Task<CurrencyDto> GetCurrency(string code, CancellationToken cancellationToken)
    {
        var currency = await _dbContext.Currencies
            .SingleOrDefaultAsync(currency => currency.Code == code, cancellationToken);
        if (currency is null) throw new CurrencyNotFoundException();

        return new CurrencyDto(currency.Code, currency.FullName);
    }

    public async Task<IEnumerable<CurrencyDto>> GetAllCurrency(CancellationToken cancellationToken)
    {
        var currencies = await _dbContext.Currencies
            .ToListAsync(cancellationToken);

        return currencies.Select(currency => new CurrencyDto(currency.Code, currency.FullName));
    }
}