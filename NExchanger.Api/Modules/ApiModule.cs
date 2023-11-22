using NExchanger.Api.Accounts;
using NExchanger.Api.Currencies;
using NExchanger.Api.Transactions;
using NExchanger.Api.Users;

namespace NExchanger.Api.Modules;

public static class ApiModule
{
    public static WebApplication MapApi(this WebApplication app)
    {
        app.MapUsersApi();
        app.MapAccountApi();
        app.MapCurrenciesApi();
        app.MapTransactionsApi();

        return app;
    }
}