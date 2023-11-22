namespace NExchanger.Domain.Transactions;

public class TransactionApplier : ITransactionApplier
{
    private readonly ITransactionMonitor _transactionMonitor;

    public TransactionApplier(ITransactionMonitor transactionMonitor)
    {
        _transactionMonitor = transactionMonitor;
    }

    public void SendTransaction(Transaction transaction)
    {
        _transactionMonitor.TransactionApplied(transaction);
    }
}