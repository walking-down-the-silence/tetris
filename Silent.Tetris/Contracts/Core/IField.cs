namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// Represents the field section on the view.
    /// </summary>
    public interface IField
    {
        /// <summary>
        /// Gets the position of the field on the view.
        /// </summary>
        Position Position { get; }

        /// <summary>
        /// Gets the size of the field on the view.
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// Gets the view for current field that is to be rendered.
        /// </summary>
        /// <returns> The <see cref="ISprite"/> instance. </returns>
        ISprite GetView();
    }
}