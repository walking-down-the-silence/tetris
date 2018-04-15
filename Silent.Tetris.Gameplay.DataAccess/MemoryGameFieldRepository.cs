using Silent.Practices.DDD;
using Silent.Practices.EventStore;
using Silent.Tetris.Core.Engine;
using System;

namespace Silent.Tetris.Gameplay.Api.Infrastructure
{
    public class MemoryGameFieldRepository : MemoryEventAggregateRepository<GameField, Guid, Event<Guid>>
    {
        public MemoryGameFieldRepository(IEventStore<Guid, Event<Guid>> eventStore) : base(eventStore)
        {
        }
    }
}
