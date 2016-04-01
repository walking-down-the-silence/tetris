using System;

namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// Game state event arguments
    /// </summary>
    public class GameStateEventArgs : EventArgs
    {
        /// <summary>
        /// Creates new instance of game state event arguments
        /// </summary>
        /// <param name="currentFigure"> The current figure that is in game field. </param>
        /// <param name="nextFigure"> The next figure that will be in game field. </param>
        /// <param name="currentScore"> The current score in game. </param>
        public GameStateEventArgs(IFigure currentFigure, IFigure nextFigure, int currentScore)
        {
            CurrentFigure = currentFigure;
            NextFigure = nextFigure;
            CurrentScore = currentScore;
        }

        /// <summary>
        /// Gets the current <see cref="IFigure"/> that is in game field.
        /// </summary>
        public IFigure CurrentFigure { get; private set; }

        /// <summary>
        /// Gets the next <see cref="IFigure"/> that will be in game field.
        /// </summary>
        public IFigure NextFigure { get; private set; }

        /// <summary>
        /// Gets the current score in game.
        /// </summary>
        public int CurrentScore { get; private set; }
    }
}