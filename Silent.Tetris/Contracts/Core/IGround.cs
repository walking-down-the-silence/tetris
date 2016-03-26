namespace Silent.Tetris.Contracts.Core
{
    public interface IGround : ISprite
    {
        IGround Merge(IFigure figure);
    }
}