using System;

namespace Silent.Tetris.Gameplay.Domain.Domain
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid Id { get; }

        public DateTime CreationDate { get; }
    }
}
