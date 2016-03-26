using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Sprites
{
    public sealed class FigureL : FigureBase
    {
        public FigureL() : base(Position.None, new[,]
                {
                    { Color.Cyan, Color.Transparent },
                    { Color.Cyan, Color.Transparent },
                    { Color.Cyan, Color.Transparent },
                    { Color.Cyan, Color.Cyan }
                })
        {
        }

        public FigureL(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}