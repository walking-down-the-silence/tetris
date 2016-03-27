using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Sprites
{
    public sealed class FigureZ : FigureBase
    {
        public FigureZ() : base(Position.None, new[,]
                {
                    { Color.Green, Color.Green, Color.Transparent },
                    { Color.Transparent, Color.Green, Color.Green }
                })
        {
        }

        public FigureZ(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}