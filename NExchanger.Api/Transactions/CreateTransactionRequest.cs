namespace NExchanger.Api.Transactions;

public record CreateTransactionRequest(
    int SourceAccountId,
    int DestinationAccountId,
    decimal Amount,
    double ExchangeRate,
    double Commission);