using Microsoft.EntityFrameworkCore;
using NExchanger.Persistence.Entities;

namespace NExchanger.Persistence;

public class NExchangerContext : DbContext
{
    public NExchangerContext(DbContextOptions<NExchangerContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Currency> Currencies { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    public async Task PopulateFakeData()
    {
        
        Currencies.Add(new()
        {
            Code = "USD",
            FullName = "USD"
        });
        Currencies.Add(new()
        {
            Code = "RUB",
            FullName = "RUB"
        });
        Currencies.Add(new()
        {
            Code = "EUR",
            FullName = "EUR"
        });

        var user = new User()
        {
            Name = "Alex",
            Email = "mail"
        };
        Users.Add(user);

        Accounts.Add(new()
        {
            CurrencyId = 1,
            Balance = 1000,
            Owner = user,
        });
        Accounts.Add(new()
        {
            CurrencyId = 2,
            Balance = 50000,
            Owner = user,
        });
        Accounts.Add(new()
        {
            CurrencyId = 3,
            Balance = 0,
            Owner = user,
        });

        await SaveChangesAsync();
    }
}