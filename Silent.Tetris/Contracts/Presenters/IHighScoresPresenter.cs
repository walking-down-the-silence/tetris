using System.Collections.Generic;

namespace Silent.Tetris.Contracts.Presenters
{
    /// <summary>
    /// Presenter for the high scores view.
    /// </summary>
    public interface IHighScoresPresenter : IPresenter
    {
        /// <summary>
        /// Gets the collection of player scores.
        /// </summary>
        ICollection<Player> HighScores { get; }
    }
}