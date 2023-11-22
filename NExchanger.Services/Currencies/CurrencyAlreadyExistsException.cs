namespace NExchanger.Services.Currencies;

public class CurrencyAlreadyExistsException : Exception
{
    public CurrencyAlreadyExistsException()
    {
    }

    public CurrencyAlreadyExistsException(string message) : base(message)
    {
    }

    public CurrencyAlreadyExistsException(string message, Exception inner) : base(message, inner)
    {
    }
}