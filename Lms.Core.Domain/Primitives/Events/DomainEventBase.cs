namespace Lms.Core.Domain.Primitives.Events
{
    public abstract class DomainEventBase : IDomainEvent
    {
        private readonly Guid _id;
        private readonly DateTime _occurredOn;

        public DomainEventBase()
        {
            _id = Guid.NewGuid();
            _occurredOn = DateTime.UtcNow;
        }

        public Guid Id
        {
            get
            {
                return _id;
            }
        }

        public DateTime OccurredOn
        {
            get
            {
                return _occurredOn;
            }
        }
    }
}
