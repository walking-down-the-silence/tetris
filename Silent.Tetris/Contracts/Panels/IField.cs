using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Contracts.Panels
{
    public interface IField
    {
        Position Position { get; }

        Size Size { get; }

        ISprite GetView();
    }
}