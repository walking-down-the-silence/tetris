using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Figures
{
    public sealed class FigureI : FigureBase
    {
        public FigureI() : base(Position.None, new[,]
                {
                    { Color.Magenta },
                    { Color.Magenta },
                    { Color.Magenta },
                    { Color.Magenta }
                })
        {
        }

        public FigureI(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}