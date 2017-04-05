namespace Silent.Tetris.Contracts.Presenters
{
    /// <summary>
    /// Presenter for the game over view.
    /// </summary>
    public interface IGameOverPresenter : IPresenter
    {
        /// <summary>
        /// Gets the score gained by player before game ended.
        /// </summary>
        int Score { get; }
    }
}