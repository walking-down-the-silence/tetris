using System.Collections.Concurrent;
using Silent.Tetris.Contracts;

namespace Silent.Tetris
{
    public class CommandBus : ICommandBus
    {
        private readonly BlockingCollection<ICommand> _commands = new BlockingCollection<ICommand>();

        public void Add(ICommand source)
        {
            _commands.Add(source);
        }

        public ICommand Take()
        {
            return _commands.Take();
        }

        public void Complete()
        {
            _commands.CompleteAdding();
        }

        public void Dispose()
        {
            _commands.Dispose();
        }
    }
}