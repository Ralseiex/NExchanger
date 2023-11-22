namespace NExchanger.Domain.Transactions;

public class Transaction
{
    private Transaction(
        int id,
        DateTime dateTime,
        int sourceAccountId,
        int destinationAccountId,
        decimal amount,
        double exchangeRate,
        double commissionRate,
        string status)
    {
        Id = id;
        DateTime = dateTime;
        SourceAccountId = sourceAccountId;
        DestinationAccountId = destinationAccountId;
        Amount = amount;
        ExchangeRate = exchangeRate;
        CommissionRate = commissionRate;
        Status = status;
    }

    public int Id { get; }
    public DateTime DateTime { get; }
    public int SourceAccountId { get; }
    public int DestinationAccountId { get; }
    public decimal Amount { get; }
    public double ExchangeRate { get; }
    public double CommissionRate { get; }
    public string Status { get; }

    public static Transaction Create(
        int sourceAccountId, 
        int destinationAccountId, 
        decimal amount, 
        double exchangeRate,
        double commissionRate) =>
        new(0, DateTime.UtcNow, sourceAccountId, destinationAccountId, amount, exchangeRate, commissionRate, "Awaits");


    public Transaction AddId(int id) =>
        new(id, DateTime, SourceAccountId, DestinationAccountId, Amount, ExchangeRate, CommissionRate, Status);
}