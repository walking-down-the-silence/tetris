using System;

namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// The player with score.
    /// </summary>
    [Serializable]
    public class ScoreRecord
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public ScoreRecord()
        {
            Date = DateTime.Now;
        }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="nickname"> The name of the player. </param>
        /// <param name="score"> The score of the player. </param>
        public ScoreRecord(string nickname, long score)
        {
            Nickname = nickname;
            Score = score;
            Date = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets the score for the player.
        /// </summary>
        public long Score { get; set; }

        /// <summary>
        /// Gets or sets the date when the score was set.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
