using System;

namespace Silent.Practices.EventStore
{
    public abstract class Event<TKey> : IEvent
    {
        protected Event()
        {
            EventId = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }

        protected Event(TKey entityId)
        {
            EventId = Guid.NewGuid();
            EntityId = entityId;
            Timestamp = DateTime.UtcNow;
        }

        public Guid EventId { get; set; }

        public TKey EntityId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
