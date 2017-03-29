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
        /// <returns> The <see cref="IDisposable"/> instance to stop the engine. </returns>
        IDisposable Run();

        /// <summary>
        /// Tries to move current figure by 1 point in specified direction.
        /// </summary>
        /// <param name="motionDirection"> Specified direction. </param>
        void MoveCurrentFigure(MotionDirection motionDirection);

        /// <summary>
        /// Rotates current figure to the right by 90 degrees.
        /// </summary>
        void RotateCurrentFigure();
    }
}