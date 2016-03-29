using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Contracts.Panels
{
    public interface IRightInfoField : IField
    {
        void AssignNextFigure(IFigure nextFigure);

        void SetScore(int currentScore);
    }
}