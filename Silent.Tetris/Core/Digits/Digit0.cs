using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Sprites;

namespace Silent.Tetris.Core.Digits
{
    public class Digit0 : FigureBase
    {
        public Digit0() : base(Position.None, new[,]
        {
            { Color.Transparent, Color.Cyan, Color.Transparent },
            { Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Transparent, Color.Cyan, Color.Transparent }
        })
        {
        }

        public Digit0(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}