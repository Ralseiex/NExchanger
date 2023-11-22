namespace NExchanger.Services.Accounts;

public interface IAccountService
{
    Task<int> CreateAccount(string ownerName, string currencyCode, CancellationToken cancellationToken);
    Task<AccountDto> GetAccount(int id, CancellationToken cancellationToken);
    Task<IEnumerable<AccountDto>> GetAllUserAccounts(string userName, CancellationToken cancellationToken);
}