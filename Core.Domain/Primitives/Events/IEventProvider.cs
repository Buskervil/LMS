using System.Collections.Concurrent;

namespace Core.Domain.Primitives.Events
{
    public interface IEventProvider
    {
        IProducerConsumerCollection<IDomainEvent> DomainEvents { get; }
    }
    
    public interface IPublicEventProvider : IEventProvider
    {
        void PublishEvent(IDomainEvent domainEvent);
    }
}