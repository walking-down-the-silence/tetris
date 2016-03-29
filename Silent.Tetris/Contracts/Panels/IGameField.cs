using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Contracts.Panels
{
    public interface IGameField : IField
    {
        IFigure CurrentFigure { get; }

        IGround Ground { get; }

        void AssignCurrentFigure(IFigure currentFigure);

        void MoveCurrentFigure(MotionDirection motionDirection);

        void RotateCurrentFigure();

        void GenerateNextFigure();
    }
}