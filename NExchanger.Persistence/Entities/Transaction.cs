using NExchanger.Persistence.Entities.Base;

namespace NExchanger.Persistence.Entities;

public class Transaction : BaseEntity<int>
{
    public DateTime DateTime { get; set; }
    public int SourceAccountId { get; set; }
    public int DestinationAccountId { get; set; }
    public decimal Amount { get; set; }
    public double ExchangeRate { get; set; }
    public double Commission { get; set; }
    public string Status { get; set; } = string.Empty;

    public Account? SourceAccount { get; set; }
    public Account? DestinationAccount { get; set; }
}