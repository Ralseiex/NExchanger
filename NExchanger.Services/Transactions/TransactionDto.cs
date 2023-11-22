namespace NExchanger.Services.Transactions;

public record TransactionDto(
    DateTime DateTime,
    int SourceAccountId,
    int DestinationAccountId,
    decimal Amount,
    double ExchangeRate,
    double Commission);