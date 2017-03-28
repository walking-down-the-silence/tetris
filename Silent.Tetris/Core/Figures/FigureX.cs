using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Figures
{
    public sealed class FigureX : FigureBase
    {
        public FigureX() : base(Position.None, new[,]
                {
                    { Color.Transparent, Color.Gray, Color.Transparent },
                    { Color.Gray, Color.Gray, Color.Gray },
                    { Color.Transparent, Color.Gray, Color.Transparent }
                })
        {
        }

        public FigureX(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}