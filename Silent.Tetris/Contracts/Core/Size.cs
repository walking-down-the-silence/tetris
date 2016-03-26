namespace Silent.Tetris.Contracts.Core
{
    public class Size
    {
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static Size None => new Size(0, 0);

        public int Width { get; }

        public int Height { get; }
    }
}