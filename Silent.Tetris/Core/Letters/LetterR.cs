using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Sprites;

namespace Silent.Tetris.Core.Letters
{
    public class LetterR : FigureBase
    {
        public LetterR() : base(Position.None, new[,]
        {
            { Color.Cyan, Color.Cyan, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Cyan, Color.Cyan },
            { Color.Cyan, Color.Cyan, Color.Transparent },
            { Color.Cyan, Color.Transparent, Color.Cyan }
        })
        {
        }

        public LetterR(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}