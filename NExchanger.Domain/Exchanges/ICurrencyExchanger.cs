namespace NExchanger.Domain.Exchanges;

public interface ICurrencyExchanger
{
    decimal Exchange(decimal amount, double exchangeRate);
}