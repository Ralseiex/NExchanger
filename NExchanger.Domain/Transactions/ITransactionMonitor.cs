namespace NExchanger.Domain.Transactions;

public interface ITransactionMonitor
{
    IDisposable Subscribe(Func<Transaction, Task> observer);
    void TransactionApplied(Transaction transaction);
}