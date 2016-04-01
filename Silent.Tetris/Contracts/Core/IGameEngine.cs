using System;
using Silent.Tetris.Contracts.Panels;

namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// Processes the in-game logic and computations.
    /// </summary>
    public interface IGameEngine
    {
        /// <summary>
        /// Notifies when the game state changes.
        /// </summary>
        event EventHandler<GameStateEventArgs> StateChanged;

        /// <summary>
        /// Runs the in-game logic.
        /// </summary>
        /// <param name="gameField"> The <see cref="IGameField"/> instance to process. </param>
        /// <returns> The <see cref="IDisposable"/> instance to stop the engine. </returns>
        IDisposable Run(IGameField gameField);
    }
}