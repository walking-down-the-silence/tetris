namespace Silent.Tetris.Contracts.Core
{
    public interface ISprite
    {
        Color this[int x, int y] { get; }

        Position Position { get; }

        Size Size { get; }
    }
}