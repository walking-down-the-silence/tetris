using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Contracts.Panels
{
    public interface IGameField : IField
    {
        IFigure CurrentFigure { get; }

        IGround Ground { get; }

        void SetCurrentFigure(IFigure currentFigure);

        void SetGround(IGround ground);
    }
}