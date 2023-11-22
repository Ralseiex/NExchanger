using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NExchanger.Domain.Commissions;
using NExchanger.Domain.Exchanges;
using NExchanger.Domain.Transactions;
using NExchanger.Persistence;

namespace NExchanger.Services.Transactions;

public class TransactionMonitorListener : IDisposable
{
    private readonly ITransactionMonitor _reporter;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ICommissionCalculator _commissionCalculator;
    private readonly ICurrencyExchanger _currencyExchanger;

    private IDisposable? _subscription;

    public TransactionMonitorListener(
        IServiceScopeFactory dbContext,
        ITransactionMonitor reporter,
        ICommissionCalculator commissionCalculator,
        ICurrencyExchanger currencyExchanger)
    {
        _scopeFactory = dbContext;
        _reporter = reporter;
        _commissionCalculator = commissionCalculator;
        _currencyExchanger = currencyExchanger;
    }

    public void StartListen()
    {
        _subscription = _reporter.Subscribe(OnTransactionApplied);
    }

    public void StopListen()
    {
        _subscription?.Dispose();
    }

    private async Task OnTransactionApplied(Transaction transaction)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<NExchangerContext>();
        var transactionEntity = await dbContext.Transactions
            .SingleAsync(t => t.Id == transaction.Id);
        var sourceAccount = await dbContext.Accounts.SingleAsync(account => account.Id == transaction.SourceAccountId);
        var destinationAccount = await dbContext.Accounts.SingleAsync(account => account.Id == transaction.DestinationAccountId);

        var destinationAmount = _currencyExchanger.Exchange(transaction.Amount, transaction.ExchangeRate);
        var commission = _commissionCalculator.GetCommission(destinationAmount, transaction.CommissionRate);

        sourceAccount.Balance -= transaction.Amount;
        destinationAccount.Balance += destinationAmount - commission;

        transactionEntity.Status = "Applied";
        await dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _subscription?.Dispose();
        GC.SuppressFinalize(this);
    }
}