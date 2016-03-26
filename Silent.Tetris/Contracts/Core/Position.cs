namespace Silent.Tetris.Contracts.Core
{
    public class Position
    {
        public Position(int left, int bottom)
        {
            Left = left;
            Bottom = bottom;
        }

        public static Position None => new Position(0, 0);

        public int Left { get; }

        public int Bottom { get; }
    }
}