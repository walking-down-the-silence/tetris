using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Sprites;

namespace Silent.Tetris.Core.Digits
{
    public class Digit4 : FigureBase
    {
        public Digit4() : base(Position.None, new[,]
        {
            { Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Cyan, Color.Cyan },
            { Color.Transparent, Color.Transparent, Color.Cyan },
            { Color.Transparent, Color.Transparent, Color.Cyan }
        })
        {
        }

        public Digit4(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}