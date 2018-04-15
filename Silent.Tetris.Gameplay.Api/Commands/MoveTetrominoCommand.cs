using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Gameplay.Api.Controllers
{
    public class MoveTetrominoCommand
    {
        public MotionDirection Direction { get; internal set; }
    }
}