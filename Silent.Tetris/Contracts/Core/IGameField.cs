namespace Silent.Tetris.Contracts.Core
{
    public interface IGameField
    {
        Size Size { get; }

        IFigure CurrentFigure { get; }

        IFigure NextFigure { get; }

        IGround Ground { get; }

        ISprite GetView();

        void MoveCurrentFigure(MotionDirection motionDirection);

        void RotateCurrentFigure();

        void GenerateNextFigure();
    }
}