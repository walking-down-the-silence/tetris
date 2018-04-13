namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// Represents the immutable figure sprite.
    /// </summary>
    public interface IFigure : ISprite
    {
        /// <summary>
        /// Sets the position of the figure and returns modified figure.
        /// </summary>
        /// <param name="position"> The <see cref="Position"/> to set in. </param>
        /// <returns> Modified figure. </returns>
        IFigure SetPosition(Position position);

        /// <summary>
        /// Rotates the figure in specified direction.
        /// </summary>
        /// <param name="rotateDirection"> The <see cref="RotateDirection"/> of the figure. </param>
        /// <returns> Modified figure. </returns>
        IFigure Rotate(RotateDirection rotateDirection);
    }
}