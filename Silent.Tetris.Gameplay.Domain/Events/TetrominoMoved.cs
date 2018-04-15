using System;
using Silent.Practices.EventStore;
using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Gameplay.Domain.Events
{
    public sealed class TetrominoMoved : Event<Guid>
    {
        public TetrominoMoved(Guid gameId, MotionDirection motionDirection) : base(gameId)
        {
            MotionDirection = motionDirection;
        }

        public MotionDirection MotionDirection { get; }
    }
}
