namespace Core.Domain.Primitives.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; }
    }
}
