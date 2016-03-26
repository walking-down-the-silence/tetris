using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Contracts.Presenters
{
    public interface IGamePresenter : IPresenter
    {
        IGameField GameField { get; }
    }
}