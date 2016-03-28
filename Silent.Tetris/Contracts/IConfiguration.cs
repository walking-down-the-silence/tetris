using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Contracts
{
    public interface IConfiguration
    {
        Position Position { get; }

        Size Size { get; }

        string Title { get; }
    }
}