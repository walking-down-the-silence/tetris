using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Figures;

namespace Silent.Tetris.Core.Digits
{
    public class Digit1 : FigureBase
    {
        public Digit1() : base(Position.None, new[,]
        {
            { Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Cyan },
            { Color.Transparent, Color.Cyan},
            { Color.Transparent, Color.Cyan },
            { Color.Transparent, Color.Cyan }
        })
        {
        }

        public Digit1(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}