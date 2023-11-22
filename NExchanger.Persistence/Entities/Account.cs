using NExchanger.Persistence.Entities.Base;

namespace NExchanger.Persistence.Entities;

public class Account : BaseEntity<int>
{
    public decimal Balance { get; set; }
    public int OwnerId { get; set; }
    public int CurrencyId { get; set; }

    public User? Owner { get; set; }
    public Currency? Currency { get; set; }
}