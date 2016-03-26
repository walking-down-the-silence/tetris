using System;
using System.Threading.Tasks;
using Silent.Tetris.Commands;
using Silent.Tetris.Contracts;

namespace Silent.Tetris.Observers
{
    public class ConsoleCommandsObserveAsync : IObserveAsync<ICommand>
    {
        private Disposable _disposable;

        public event EventHandler<ICommand> Update;

        public IDisposable ObserveAsync()
        {
            _disposable = new Disposable();

            Task.Run(() =>
            {
                while (!_disposable.IsDisposed)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    OnUpdate(new ConsoleCommand(keyInfo.Key));
                }
            });

            
            return _disposable;
        }

        protected virtual void OnUpdate(ICommand e)
        {
            Update?.Invoke(this, e);
        }

        private sealed class Disposable : IDisposable
        {
            public bool IsDisposed { get; private set; }

            public void Dispose()
            {
                IsDisposed = true;
            }
        }
    }
}
