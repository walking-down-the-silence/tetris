using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris
{
    public interface IConfiguration
    {
        Position Position { get; }

        Size Size { get; }

        string Title { get; }
    }
}