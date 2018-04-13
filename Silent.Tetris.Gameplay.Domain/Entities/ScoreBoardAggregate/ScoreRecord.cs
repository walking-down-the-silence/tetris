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
        /// <param name="gameId"> The game instance identifier. </param>
        /// <param name="nickname"> The name of the player. </param>
        /// <param name="points"> The score of the player. </param>
        public ScoreRecord(Guid gameId, string nickname, long points)
        {
            GameId = gameId;
            Nickname = nickname;
            Points = points;
            Date = DateTime.Now;
        }

        /// <summary>
        /// Gets the game identifier for current score.
        /// </summary>
        public Guid GameId { get; }

        /// <summary>
        /// Gets the name of the player.
        /// </summary>
        public string Nickname { get; }

        /// <summary>
        /// Gets the score for the player.
        /// </summary>
        public long Points { get; }

        /// <summary>
        /// Gets the date when the score was set.
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Clones a record with an updated score points.
        /// </summary>
        /// <param name="points"> New score points value. </param>
        /// <returns> Cloned instance. </returns>
        public ScoreRecord UpdatePoints(long points)
        {
            return new ScoreRecord(GameId, Nickname, Points);
        }
    }
}
