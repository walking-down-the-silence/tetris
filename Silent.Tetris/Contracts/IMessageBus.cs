using System;

namespace Silent.Tetris.Contracts
{
    public interface IMessageBus<TSource> : IDisposable
    {
        void Add(TSource source);

        TSource Take();

        void Complete();
    }
}
