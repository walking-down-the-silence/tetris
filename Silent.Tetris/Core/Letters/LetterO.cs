using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Sprites;

namespace Silent.Tetris.Core.Letters
{
    public class LetterO : FigureBase
    {
        public LetterO() : base(Position.None, new[,]
        {
            { Color.Cyan, Color.Cyan, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Cyan, Color.Cyan }
        })
        {
        }

        public LetterO(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}