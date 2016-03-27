using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Sprites;

namespace Silent.Tetris.Core.Digits
{
    public class Digit2 : FigureBase
    {
        public Digit2() : base(Position.None, new[,]
        {
            { Color.Cyan, Color.Cyan, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Transparent, Color.Cyan, Color.Transparent },
            { Color.Cyan, Color.Transparent, Color.Transparent },
            { Color.Cyan, Color.Cyan, Color.Cyan }
        })
        {
        }

        public Digit2(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}