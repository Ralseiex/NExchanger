namespace NExchanger.Domain.Transactions;

public record ValidationResult(bool IsValid, string[] ErrorMessages);

public class TransactionValidator : ITransactionValidator
{
    public ValidationResult Validate(Transaction transaction)
    {
        var isValid = true;
        var errors = new List<string>();
        if (transaction.Amount <= 0)
        {
            isValid = false;
            errors.Add("Amount must be greater than 0");
        }

        if (transaction.ExchangeRate <= 0)
        {
            isValid = false;
            errors.Add("ExchangeRate must be greater than 0");
        }

        if (transaction.CommissionRate <= 0)
        {
            isValid = false;
            errors.Add("Commission must be greater or equal 0");
        }

        return new ValidationResult(isValid, errors.ToArray());
    }
}