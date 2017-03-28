using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Contracts.Presenters
{
    /// <summary>
    /// Presetner for the game view.
    /// </summary>
    public interface IGamePresenter : IPresenter
    {
        /// <summary>
        /// Gets the current game field.
        /// </summary>
        IGameField Field { get; }

        /// <summary>
        /// Gets the current game state.
        /// </summary>
        IGameState State { get; }
    }
}