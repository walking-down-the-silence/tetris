using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Figures;

namespace Silent.Tetris.Core.Digits
{
    public class Digit3 : FigureBase
    {
        public Digit3() : base(Position.None, new[,]
        {
            { Color.Cyan, Color.Cyan, Color.Transparent },
            { Color.Transparent, Color.Transparent, Color.Cyan },
            { Color.Transparent, Color.Cyan, Color.Transparent },
            { Color.Transparent, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Cyan, Color.Transparent }
        })
        {
        }

        public Digit3(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}