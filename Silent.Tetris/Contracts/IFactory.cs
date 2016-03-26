using System.Collections.Generic;

namespace Silent.Tetris.Contracts
{
    public interface IFactory<out TOutput>
    {
        IEnumerable<TOutput> Create();
    }
}
