using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Sprites;

namespace Silent.Tetris.Core.Digits
{
    public class Digit5 : FigureBase
    {
        public Digit5() : base(Position.None, new[,]
        {
            { Color.Cyan, Color.Cyan, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Transparent },
            { Color.Cyan, Color.Cyan, Color.Transparent },
            { Color.Transparent, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Cyan, Color.Transparent }
        })
        {
        }

        public Digit5(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}