namespace NExchanger.Domain.Exchanges;

public class CurrencyExchanger : ICurrencyExchanger
{
    public decimal Exchange(decimal amount, double exchangeRate)
    {
        return amount * (decimal)exchangeRate;
    }
}