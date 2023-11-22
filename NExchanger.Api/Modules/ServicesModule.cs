using NExchanger.Services.Accounts;
using NExchanger.Services.Currencies;
using NExchanger.Services.Transactions;
using NExchanger.Services.Users;

namespace NExchanger.Api.Modules;

public static class ServicesModule
{
    public static IServiceCollection AddServices(this IServiceCollection services) => services
            .AddScoped<IAccountService, AccountService>()
            .AddScoped<ICurrencyService, CurrencyService>()
            .AddScoped<ITransactionService, TransactionService>()
            .AddScoped<IUserService, UserService>();
}