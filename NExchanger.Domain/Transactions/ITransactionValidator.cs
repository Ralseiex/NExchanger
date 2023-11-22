namespace NExchanger.Domain.Transactions;

public interface ITransactionValidator
{
    ValidationResult Validate(Transaction transaction);
}