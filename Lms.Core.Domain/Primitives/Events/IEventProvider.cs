using System.Collections.Concurrent;

namespace Lms.Core.Domain.Primitives.Events
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