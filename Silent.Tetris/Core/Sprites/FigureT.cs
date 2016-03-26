using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Sprites
{
    public sealed class FigureT : FigureBase
    {
        public FigureT() : base(Position.None, new[,]
                {
                    { Color.Cyan, Color.Cyan, Color.Cyan },
                    { Color.Transparent, Color.Cyan, Color.Transparent },
                    { Color.Transparent, Color.Cyan, Color.Transparent },
                    { Color.Transparent, Color.Cyan, Color.Transparent }
                })
        {
        }

        public FigureT(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}