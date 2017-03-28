using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Figures;

namespace Silent.Tetris.Core.Digits
{
    public class Digit7 : FigureBase
    {
        public Digit7() : base(Position.None, new[,]
        {
            { Color.Cyan, Color.Cyan, Color.Cyan },
            { Color.Transparent, Color.Transparent, Color.Cyan },
            { Color.Transparent, Color.Cyan, Color.Cyan },
            { Color.Cyan, Color.Cyan, Color.Transparent },
            { Color.Cyan, Color.Transparent, Color.Transparent }
        })
        {
        }

        public Digit7(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}