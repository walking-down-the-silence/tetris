using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Figures
{
    public sealed class FigureL : FigureBase
    {
        public FigureL() : base(Position.None, new[,]
                {
                    { Color.Blue, Color.Transparent },
                    { Color.Blue, Color.Transparent },
                    { Color.Blue, Color.Transparent },
                    { Color.Blue, Color.Blue }
                })
        {
        }

        public FigureL(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}