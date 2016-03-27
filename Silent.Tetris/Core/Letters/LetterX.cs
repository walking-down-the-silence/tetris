using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Sprites;

namespace Silent.Tetris.Core.Letters
{
    public class LetterX : FigureBase
    {
        public LetterX() : base(Position.None, new[,]
        {
            { Color.Cyan, Color.Transparent,Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Transparent,Color.Cyan, Color.Cyan },
            { Color.Transparent, Color.Cyan,Color.Cyan, Color.Transparent },
            { Color.Cyan, Color.Cyan,Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Transparent,Color.Transparent, Color.Cyan }
        })
        {
        }

        public LetterX(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}