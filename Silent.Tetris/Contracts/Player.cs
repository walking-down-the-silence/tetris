using System;

namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// The player with score.
    /// </summary>
    [Serializable]
    public class Player
    {
        /// <summary>
        /// Creates new instance.
        /// </summary>
        public Player()
        {
            Date = DateTime.Now;
        }

        /// <summary>
        /// Creates new instance.
        /// </summary>
        /// <param name="name"> The name of the player. </param>
        /// <param name="score"> The score of the player. </param>
        public Player(string name, long score)
        {
            Name = name;
            Score = score;
            Date = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the name of the player.
        /// </summary>
        public string Name { get; set; }

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
