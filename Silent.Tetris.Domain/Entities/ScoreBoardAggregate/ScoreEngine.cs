using System;
using System.Collections.Generic;

namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// The score engine that is calculating and keeping track of score for each game instance.
    /// </summary>
    public class ScoreEngine
    {
        private readonly Dictionary<Guid, ScoreRecord> _activeScores = new Dictionary<Guid, ScoreRecord>();

        /// <summary>
        /// Start recording new score for new game instance.
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="nickname"></param>
        public void BeginScoreRecord(Guid gameId, string nickname)
        {
            _activeScores[gameId] = new ScoreRecord(nickname, 0);
        }

        /// <summary>
        /// Updates the score for existing game instance.
        /// </summary>
        /// <param name="gameId"> The identifier of the game instance. </param>
        /// <param name="rowsCompleted"> The amount of rows that were completed. </param>
        public void UpdateScore(Guid gameId, int rowsCompleted)
        {
            if (_activeScores.ContainsKey(gameId))
            {
                const int ScoreMultiplier = 100;
                ScoreRecord previousScore = _activeScores[gameId];
                long newScore = previousScore.Score + rowsCompleted * ScoreMultiplier;
                _activeScores[gameId] = new ScoreRecord(previousScore.Nickname, newScore);
            }
        }
    }
}
