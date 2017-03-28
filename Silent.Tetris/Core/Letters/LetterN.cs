using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Figures;

namespace Silent.Tetris.Core.Letters
{
    public class LetterN : FigureBase
    {
        public LetterN() : base(Position.None, new[,]
        {
            { Color.Cyan, Color.Transparent, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Cyan, Color.Transparent, Color.Cyan },
            { Color.Cyan, Color.Cyan, Color.Cyan, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Cyan, Color.Cyan },
            { Color.Cyan, Color.Transparent, Color.Transparent, Color.Cyan }
        })
        {
        }

        public LetterN(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}