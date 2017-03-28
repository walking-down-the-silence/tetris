using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// Represents the configuration of an application.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets the position of the window.
        /// </summary>
        Position Position { get; }

        /// <summary>
        /// Gets the size of the window.
        /// </summary>
        Size GameFieldSize { get; }

        /// <summary>
        /// Gets the title for window.
        /// </summary>
        string Title { get; }
    }
}