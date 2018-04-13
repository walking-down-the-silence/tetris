using System.Collections.Generic;

namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// The score board with list of highest scores.
    /// </summary>
    public class ScoreBoard
    {
        /// <summary>
        /// Gets or sets the collection of highest scores.
        /// </summary>
        public ICollection<ScoreRecord> HighScores { get; set; }
    }
}
