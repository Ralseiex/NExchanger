using Microsoft.EntityFrameworkCore;
using NExchanger.Persistence;
using NExchanger.Persistence.Entities;
using NExchanger.Services.Currencies;
using NExchanger.Services.Users;

namespace NExchanger.Services.Accounts;

public class AccountService : IAccountService
{
    private readonly NExchangerContext _dbContext;

    public AccountService(NExchangerContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CreateAccount(string ownerName, string currencyCode, CancellationToken cancellationToken)
    {
        var owner = await _dbContext.Users
            .SingleOrDefaultAsync(user => user.Name == ownerName, cancellationToken);
        if (owner is null) throw new UserNotFoundException();
        var currency = await _dbContext.Currencies
            .SingleOrDefaultAsync(currency => currency.Code == currencyCode, cancellationToken);
        if (currency is null) throw new CurrencyNotFoundException();

        var account = new Account()
        {
            Created = DateTime.UtcNow,
            Currency = currency,
            Balance = 0
        };
        owner.Accounts.Add(account);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return account.Id;
    }

    public async Task<AccountDto> GetAccount(int id, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            .Include(account => account.Owner)
            .Include(account => account.Currency)
            .SingleOrDefaultAsync(account => account.Id == id, cancellationToken);
        if (account is null) throw new AccountNotFoundException();

        return new AccountDto(account.Id, account.Owner!.Name, account.Currency!.Code, account.Balance);
    }

    public async Task<IEnumerable<AccountDto>> GetAllUserAccounts(string userName, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(user => user.Accounts)
                .ThenInclude(account => account.Currency!)
            .SingleOrDefaultAsync(user => user.Name == userName, cancellationToken);
        if (user is null) throw new UserNotFoundException();
        return user.Accounts.Select(
            account => new AccountDto(account.Id, account.Owner!.Name, account.Currency!.Code, account.Balance));
    }
}