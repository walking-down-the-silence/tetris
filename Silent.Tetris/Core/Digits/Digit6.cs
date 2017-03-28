using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Figures;

namespace Silent.Tetris.Core.Digits
{
    public class Digit6 : FigureBase
    {
        public Digit6() : base(Position.None, new[,]
        {
            { Color.Transparent, Color.Cyan, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Transparent },
            { Color.Cyan, Color.Cyan, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Cyan, Color.Cyan }
        })
        {
        }

        public Digit6(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}