namespace Silent.Tetris.Contracts.Core
{
    public interface IGameFieldRow
    {
        Color this[int index] { get; set; }

        bool IsComplete();
    }
}