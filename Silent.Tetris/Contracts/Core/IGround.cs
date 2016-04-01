namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// Represents the ground sprite.
    /// </summary>
    public interface IGround : ISprite
    {
        /// <summary>
        /// Merges the ground with specified <see cref="IFigure"/>.
        /// </summary>
        /// <param name="figure"> The <see cref="IFigure"/> instance. </param>
        /// <returns> Modified ground figure. </returns>
        IGround Merge(IFigure figure);
    }
}