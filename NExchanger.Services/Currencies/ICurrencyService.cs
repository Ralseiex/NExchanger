namespace NExchanger.Services.Currencies;

public interface ICurrencyService
{
    Task<int> CreateCurrency(string code, string fullName, CancellationToken cancellationToken);
    Task<CurrencyDto> GetCurrency(string code, CancellationToken cancellationToken);
    Task<IEnumerable<CurrencyDto>> GetAllCurrency(CancellationToken cancellationToken);
}