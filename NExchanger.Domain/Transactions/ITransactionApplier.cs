namespace NExchanger.Domain.Transactions;

public interface ITransactionApplier
{
    void SendTransaction(Transaction transaction);
}