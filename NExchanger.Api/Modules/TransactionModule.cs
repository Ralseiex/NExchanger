using NExchanger.Domain.Commissions;
using NExchanger.Domain.Exchanges;
using NExchanger.Domain.Transactions;
using NExchanger.Services.Transactions;

namespace NExchanger.Api.Modules;

public static class TransactionModule
{
    public static IServiceCollection AddTransactions(this IServiceCollection services) => services
        .AddSingleton<ITransactionApplier, TransactionApplier>()
        .AddSingleton<ITransactionMonitor, TransactionMonitor>()
        .AddSingleton<TransactionMonitorListener>()
        .AddSingleton<ITransactionValidator, TransactionValidator>()
        .AddSingleton<ICommissionCalculator, CommissionCalculator>()
        .AddSingleton<ICurrencyExchanger, CurrencyExchanger>();

    public static IApplicationBuilder StartTransactionListener(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var listener = scope.ServiceProvider.GetRequiredService<TransactionMonitorListener>();
        listener.StartListen();
        return app;
    }
}