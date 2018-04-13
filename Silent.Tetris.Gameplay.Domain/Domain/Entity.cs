using System;

namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// Represents an entity with an identifier.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid Id { get; set; }
    }
}