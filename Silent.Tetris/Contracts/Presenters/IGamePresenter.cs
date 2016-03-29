using Silent.Tetris.Contracts.Panels;

namespace Silent.Tetris.Contracts.Presenters
{
    public interface IGamePresenter : IPresenter
    {
        IField GameField { get; }

        IField RightInfoField { get; }
    }
}