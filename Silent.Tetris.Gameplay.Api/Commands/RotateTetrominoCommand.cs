using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Gameplay.Api.Controllers
{
    public class RotateTetrominoCommand
    {
        public RotateDirection Direction { get; internal set; }
    }
}