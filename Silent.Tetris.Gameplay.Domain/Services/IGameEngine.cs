using System;

namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// Processes the in-game logic and computations.
    /// </summary>
    public interface IGameEngine
    {
        /// <summary>
        /// Gets the state of the game engine.
        /// </summary>
        GameEngineState State { get; }

        /// <summary>
        /// Notifies when the game state changes.
        /// </summary>
        event EventHandler<GameStateEventArgs> StateChanged;

        /// <summary>
        /// Runs the in-game logic.
        /// </summary>
        /// <param name="gameId"> Game instance identifier. </param>
        /// <returns> The <see cref="IDisposable"/> instance to stop the engine. </returns>
        IDisposable Run(Guid gameId);

        /// <summary>
        /// Pauses the engine processing the game.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes the engine processing the game.
        /// </summary>
        void Resume();

        /// <summary>
        /// End the game and stops the engine.
        /// </summary>
        void End();

        /// <summary>
        /// Checks if game is over.
        /// </summary>
        /// <returns> Game over indicator. </returns>
        bool IsGameOver();
    }
}