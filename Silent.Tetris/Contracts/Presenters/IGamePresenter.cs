using Silent.Tetris.Contracts.Panels;

namespace Silent.Tetris.Contracts.Presenters
{
    /// <summary>
    /// Presetner for the game view.
    /// </summary>
    public interface IGamePresenter : IPresenter
    {
        /// <summary>
        /// Gets the game field as <see cref="IField"/>.
        /// </summary>
        IField GameField { get; }

        /// <summary>
        /// Gets the info field as <see cref="IField"/>.
        /// </summary>
        IField RightInfoField { get; }
    }
}