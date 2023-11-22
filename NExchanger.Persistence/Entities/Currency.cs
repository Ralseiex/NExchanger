using NExchanger.Persistence.Entities.Base;

namespace NExchanger.Persistence.Entities;

public class Currency : BaseEntity<int>
{
    public required string Code { get; set; }
    public required string FullName { get; set; }
}