namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// Represents the game field section on the view.
    /// </summary>
    public interface IGameField : IField
    {
        /// <summary>
        /// Gets the current <see cref="IFigure"/> that is in-game.
        /// </summary>
        IFigure CurrentFigure { get; }

        /// <summary>
        /// Gets the current <see cref="IGround"/> that is in-game.
        /// </summary>
        IGround Ground { get; }

        /// <summary>
        /// Sets the current <see cref="IFigure"/> for game field.
        /// </summary>
        /// <param name="currentFigure"> The <see cref="IFigure"/> instance. </param>
        void SetCurrentFigure(IFigure currentFigure);

        /// <summary>
        /// Sets the current <see cref="IGround"/> for game field.
        /// </summary>
        /// <param name="ground"> The <see cref="IGround"/> instance. </param>
        void SetGround(IGround ground);
    }
}