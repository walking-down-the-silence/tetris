using System;
using Silent.Practices.EventStore;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Gameplay.Domain.Events
{
    public sealed class GameCreated : Event<Guid>
    {
        public GameCreated(Guid gameId, Size size) : base(gameId)
        {
            Size = size;
        }

        public Size Size { get; }
    }
}
