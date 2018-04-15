using System;
using Silent.Practices.EventStore;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Gameplay.Domain.Events
{
    public sealed class TetrominoRotated : Event<Guid>
    {
        public TetrominoRotated(Guid gameId, RotateDirection rotateDirection) : base(gameId)
        {
            RotateDirection = rotateDirection;
        }

        public RotateDirection RotateDirection { get; }
    }
}
