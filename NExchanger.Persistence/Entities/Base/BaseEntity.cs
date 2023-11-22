namespace NExchanger.Persistence.Entities.Base;

public abstract class BaseEntity<T> where T : struct
{
    public T Id { get; init; }
    public DateTime Created { get; init; } = DateTime.UtcNow;
}