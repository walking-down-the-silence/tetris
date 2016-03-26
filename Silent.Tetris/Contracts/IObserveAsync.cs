using System;

namespace Silent.Tetris.Contracts
{
    public interface IObserveAsync<TOuput>
    {
        event EventHandler<TOuput> Update;

        IDisposable ObserveAsync();
    }
}