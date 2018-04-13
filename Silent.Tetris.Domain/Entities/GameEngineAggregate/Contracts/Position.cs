namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// Represents the position of an object as a margin.
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Creates new instance of position.
        /// </summary>
        /// <param name="left"> The left margin. </param>
        /// <param name="bottom"> The bottom margin. </param>
        public Position(int left, int bottom)
        {
            Left = left;
            Bottom = bottom;
        }

        /// <summary>
        /// Gets the position instance with zero margin.
        /// </summary>
        public static Position None => new Position(0, 0);

        /// <summary>
        /// Gets the left margin.
        /// </summary>
        public int Left { get; }

        /// <summary>
        /// Gets the bottom margin.
        /// </summary>
        public int Bottom { get; }
    }
}