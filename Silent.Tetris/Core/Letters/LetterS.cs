using Silent.Tetris.Contracts.Core;
using Silent.Tetris.Core.Sprites;

namespace Silent.Tetris.Core.Letters
{
    public class LetterS : FigureBase
    {
        public LetterS() : base(Position.None, new[,]
                {
                    { Color.Cyan, Color.Cyan, Color.Cyan },
                    { Color.Cyan, Color.Transparent, Color.Transparent },
                    { Color.Cyan, Color.Cyan, Color.Cyan },
                    { Color.Transparent, Color.Transparent, Color.Cyan },
                    { Color.Cyan, Color.Cyan, Color.Cyan }
                })
        {
        }

        public LetterS(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}
