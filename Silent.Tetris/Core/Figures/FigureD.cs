using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Figures
{
    public sealed class FigureD : FigureBase
    {
        public FigureD() : base(Position.None, new[,] { { Color.Cyan } })
        {
        }

        public FigureD(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}