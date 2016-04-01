using Silent.Tetris.Contracts.Core;

namespace Silent.Tetris.Contracts.Panels
{
    /// <summary>
    /// Represents the single row for the <see cref="IGround"/> figure.
    /// </summary>
    public interface IGameFieldRow
    {
        /// <summary>
        /// Gets or sets the row cell <see cref="Color"/>.
        /// </summary>
        /// <param name="index"> The index of the cell. </param>
        /// <returns></returns>
        Color this[int index] { get; set; }

        /// <summary>
        /// Checks if row is completely filled.
        /// </summary>
        /// <returns> Returns true if row is complete. </returns>
        bool IsComplete();
    }
}