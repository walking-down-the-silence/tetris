using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Contracts.Panels
{
    public interface IGameFieldRow
    {
        Color this[int index] { get; set; }

        bool IsComplete();
    }
}