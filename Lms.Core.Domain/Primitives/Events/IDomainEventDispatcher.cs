namespace Lms.Core.Domain.Primitives.Events
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch(IEnumerable<IDomainEvent> domainEvents);
    }

    public interface IOutboxEventDispatcher
    {
        Task Dispatch(IEnumerable<IDomainEvent> domainEvents);
    }
}
