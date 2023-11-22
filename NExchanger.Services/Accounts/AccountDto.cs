namespace NExchanger.Services.Accounts;

public record AccountDto(int Id, string OwnerName, string CurrencyCode, decimal Balance);