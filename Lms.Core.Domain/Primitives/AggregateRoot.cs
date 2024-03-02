using System.Collections.Concurrent;
using Lms.Core.Domain.Primitives.Events;

namespace Lms.Core.Domain.Primitives
{
    public abstract class AggregateRoot : Entity, IEventProvider
    {
        private readonly ConcurrentQueue<IDomainEvent> _domainEvents = new ConcurrentQueue<IDomainEvent>();

        protected AggregateRoot()
        {
        }

        public IProducerConsumerCollection<IDomainEvent> DomainEvents
        {
            get { return _domainEvents; }
        }

        protected void PublishEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Enqueue(domainEvent);
        }
    }
}
