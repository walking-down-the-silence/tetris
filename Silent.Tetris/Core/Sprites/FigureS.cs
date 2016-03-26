using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Core.Sprites
{
    public sealed class FigureS : FigureBase
    {
        public FigureS() : base(Position.None, new[,]
                {
                    { Color.Cyan, Color.Cyan, Color.Cyan },
                    { Color.Cyan, Color.Cyan, Color.Cyan },
                    { Color.Cyan, Color.Cyan, Color.Cyan }
                })
        {
        }

        public FigureS(Position position, Color[,] cells) : base(position, cells)
        {
        }
    }
}