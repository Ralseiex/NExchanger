namespace NExchanger.Domain.Transactions;

public class TransactionMonitor : ITransactionMonitor
{
    private readonly HashSet<Func<Transaction, Task>> _observers = new();

    private class Unsubscriber : IDisposable
    {
        private readonly HashSet<Func<Transaction, Task>> _observers;
        private readonly Func<Transaction, Task> _observer;

        public Unsubscriber(HashSet<Func<Transaction, Task>> observers, Func<Transaction, Task> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            _observers.Remove(_observer);
        }
    }

    public void TransactionApplied(Transaction transaction)
    {
        foreach (var observer in _observers)
        {
            observer.Invoke(transaction);
        }
    }

    public IDisposable Subscribe(Func<Transaction, Task> observer)
    {
        _observers.Add(observer);
        return new Unsubscriber(_observers, observer);
    }
}