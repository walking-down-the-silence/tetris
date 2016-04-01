namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// Represents the command message bus.
    /// </summary>
    public interface ICommandBus : IMessageBus<ICommand>
    {
    }
}