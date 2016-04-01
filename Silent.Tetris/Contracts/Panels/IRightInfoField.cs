using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Contracts.Panels
{
    /// <summary>
    /// Represents the info field section.
    /// </summary>
    public interface IRightInfoField : IField
    {
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