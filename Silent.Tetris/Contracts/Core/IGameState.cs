namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// Represents the game state information
    /// </summary>
    public interface IGameState : IField
    {
        /// <summary>
        /// Gets the current <see cref="IFigure"/> that is in game field.
        /// </summary>
        IFigure CurrentFigure { get; }

        /// <summary>
        /// Gets the next <see cref="IFigure"/> that will be in game field.
        /// </summary>
        IFigure NextFigure { get; }

        /// <summary>
        /// Gets the current score in game.
        /// </summary>
        int CurrentScore { get; }

        /// <summary>
        /// Sets the next figure that is goin to be in game.
        /// </summary>
        /// <param name="nextFigure"> The <see cref="IFigure"/> instance. </param>
        void AssignNextFigure(IFigure nextFigure);

        /// <summary>
        /// Sets the current score for the game.
        /// </summary>
        /// <param name="currentScore"></param>
        void SetScore(int currentScore);
    }
}