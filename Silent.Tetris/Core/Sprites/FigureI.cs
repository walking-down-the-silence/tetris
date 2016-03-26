using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Sprites
{
    public sealed class FigureI : FigureBase
    {
        public FigureI() : base(Position.None, new[,]
                {
                    { Color.Cyan },
                    { Color.Cyan },
                    { Color.Cyan },
                    { Color.Cyan }
                })
        {
        }

        public FigureI(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}