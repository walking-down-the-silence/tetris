using System;

namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// Processes the in-game logic and computations.
    /// </summary>
    public interface IGameEngine
    {
        /// <summary>
        /// Gets the current game field.
        /// </summary>
        IGameField Field { get; }

        /// <summary>
        /// Gets the current game state.
        /// </summary>
        IGameState State { get; }

        /// <summary>
        /// Notifies when the game state changes.
        /// </summary>
        event EventHandler<GameStateEventArgs> StateChanged;

        /// <summary>
        /// Runs the in-game logic.
        /// </summary>
        /// <param name="gameField"> The <see cref="IGameField"/> instance to process. </param>
        /// <param name="gameState"> The <see cref="IGameState"/> instance for persisting state. </param>
        /// <returns> The <see cref="IDisposable"/> instance to stop the engine. </returns>
        IDisposable Run(IGameField gameField, IGameState gameState);
    }
}