namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// Represents a command object.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        string Name { get; }
    }
}