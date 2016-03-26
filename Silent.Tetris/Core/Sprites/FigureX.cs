using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Sprites
{
    public sealed class FigureX : FigureBase
    {
        public FigureX() : base(Position.None, new[,]
                {
                    { Color.Transparent, Color.Cyan, Color.Transparent },
                    { Color.Cyan, Color.Cyan, Color.Cyan },
                    { Color.Transparent, Color.Cyan, Color.Transparent }
                })
        {
        }

        public FigureX(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}