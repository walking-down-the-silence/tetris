using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Sprites
{
    public sealed class FigureT : FigureBase
    {
        public FigureT() : base(Position.None, new[,]
                {
                    { Color.Yellow, Color.Yellow, Color.Yellow },
                    { Color.Transparent, Color.Yellow, Color.Transparent },
                    { Color.Transparent, Color.Yellow, Color.Transparent },
                    { Color.Transparent, Color.Yellow, Color.Transparent }
                })
        {
        }

        public FigureT(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}