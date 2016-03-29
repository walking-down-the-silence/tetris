using System.Collections.Generic;

namespace Silent.Tetris.Contracts
{
    public interface IFactory<out TResult>
    {
        IEnumerable<TResult> Create();
    }

    public interface IFactory<out TResult, in TSource>
    {
        TResult Create(TSource source);
    }
}
