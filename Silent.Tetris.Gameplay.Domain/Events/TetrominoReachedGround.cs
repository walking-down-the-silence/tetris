using Silent.Practices.EventStore;
using System;

namespace Silent.Tetris.Gameplay.Domain.Events
{
    public sealed class TetrominoReachedGround : Event<Guid>
    {
        public TetrominoReachedGround(Guid gameId, int rowsCompleted) : base(gameId)
        {
            RowsCompleted = rowsCompleted;
        }

        public int RowsCompleted { get; }
    }
}
