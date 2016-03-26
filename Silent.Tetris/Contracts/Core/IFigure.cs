namespace Silent.Tetris.Contracts.Core
{
    public interface IFigure : ISprite
    {
        IFigure SetPosition(Position position);

        IFigure Rotate(RotateDirection rotateDirection);
    }
}