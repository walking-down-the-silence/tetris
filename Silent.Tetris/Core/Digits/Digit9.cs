using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Figures;

namespace Silent.Tetris.Core.Digits
{
    public class Digit9 : FigureBase
    {
        public Digit9() : base(Position.None, new[,]
        {
            { Color.Cyan, Color.Cyan, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Cyan, Color.Cyan },
            { Color.Transparent, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Cyan, Color.Transparent }
        })
        {
        }

        public Digit9(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}