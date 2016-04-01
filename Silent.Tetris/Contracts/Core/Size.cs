namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// Represents teh size of an object.
    /// </summary>
    public class Size
    {
        /// <summary>
        /// Creates new instance of size.
        /// </summary>
        /// <param name="width"> The width of the object. </param>
        /// <param name="height"> The height of the object. </param>
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets the new instance of zero size.
        /// </summary>
        public static Size None => new Size(0, 0);

        /// <summary>
        /// Gets the width of an object.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height of an object.
        /// </summary>
        public int Height { get; }
    }
}