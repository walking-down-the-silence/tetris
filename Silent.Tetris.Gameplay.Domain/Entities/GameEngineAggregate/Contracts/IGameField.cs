using Silent.Practices.Persistance;
using System;

namespace Silent.Tetris.Contracts.Core
{
    /// <summary>
    /// Represents the game field section on the view.
    /// </summary>
    public interface IGameField : IField, IEntity<Guid>
    {
        /// <summary>
        /// Gets the current <see cref="IFigure"/> that is in-game.
        /// </summary>
        IFigure CurrentFigure { get; }

        /// <summary>
        /// Gets the next generated <see cref="IFigure"/> for the game.
        /// </summary>
        IFigure NextFigure { get; }

        /// <summary>
        /// Gets the current <see cref="IGround"/> that is in-game.
        /// </summary>
        IGround Ground { get; }
        
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