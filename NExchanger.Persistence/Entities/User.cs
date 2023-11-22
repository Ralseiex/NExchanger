using NExchanger.Persistence.Entities.Base;

namespace NExchanger.Persistence.Entities;

public class User : BaseEntity<int>
{
    public required string Name { get; set; }
    public required string Email { get; set; }

    public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();
}