using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Sprites
{
    public sealed class FigureZ : FigureBase
    {
        public FigureZ() : base(Position.None, new[,]
                {
                    { Color.Cyan, Color.Cyan, Color.Transparent },
                    { Color.Transparent, Color.Cyan, Color.Cyan }
                })
        {
        }

        public FigureZ(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}