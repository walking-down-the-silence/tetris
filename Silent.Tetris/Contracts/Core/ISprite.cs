namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// REpresents the basic sprite in game.
    /// </summary>
    public interface ISprite
    {
        /// <summary>
        /// Gets the color of sprite cell.
        /// </summary>
        /// <param name="x"> The x coordinate. </param>
        /// <param name="y"> The y coordinate. </param>
        /// <returns></returns>
        Color this[int x, int y] { get; }

        /// <summary>
        /// Gets the position of the sprite.
        /// </summary>
        Position Position { get; }

        /// <summary>
        /// Gets the size of the sprite.
        /// </summary>
        Size Size { get; }
    }
}