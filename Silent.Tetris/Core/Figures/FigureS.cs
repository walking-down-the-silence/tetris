using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Figures
{
    public sealed class FigureS : FigureBase
    {
        public FigureS() : base(Position.None, new[,]
                {
                    { Color.Red, Color.Red },
                    { Color.Red, Color.Red }
                })
        {
        }

        public FigureS(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}