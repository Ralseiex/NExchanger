namespace NExchanger.Services.Transactions;

public interface ITransactionService
{
    Task<int> CreateTransaction(
        int sourceAccountId,
        int destinationAccountId,
        decimal amount,
        double exchangeRate,
        double commission,
        CancellationToken cancellationToken);

    Task<TransactionDto> GetTransaction(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TransactionDto>> GetAllTransaction(CancellationToken cancellationToken);
}