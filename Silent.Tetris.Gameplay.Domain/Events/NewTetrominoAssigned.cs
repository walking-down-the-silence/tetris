using Silent.Practices.EventStore;
using System;

namespace Silent.Tetris.Gameplay.Domain.Events
{
    public sealed class NewTetrominoAssigned : Event<Guid>
    {
        public NewTetrominoAssigned(Guid gameId, string tetrominoType) : base(gameId)
        {
            TetrominoType = tetrominoType;
        }

        public string TetrominoType { get; }
    }
}
