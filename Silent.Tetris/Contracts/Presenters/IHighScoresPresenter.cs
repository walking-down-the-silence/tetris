using System.Collections.Generic;

namespace Silent.Tetris.Contracts.Presenters
{
    public interface IHighScoresPresenter : IPresenter
    {
        ICollection<Player> HighScores { get; }
    }
}