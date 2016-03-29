using System;

namespace Silent.Tetris.Contracts
{
    [Serializable]
    public class Player
    {
        public Player()
        {
            Date = DateTime.Now;
        }

        public Player(string name, long score)
        {
            Name = name;
            Score = score;
            Date = DateTime.Now;
        }

        public string Name { get; set; }

        public long Score { get; set; }

        public DateTime Date { get; set; }
    }
}
