using System.Collections.Generic;

namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// Represents the factory pattern for creating a family objects.
    /// </summary>
    /// <typeparam name="TResult"> The base type of object family. </typeparam>
    public interface IFactory<out TResult>
    {
        /// <summary>
        /// Creates a set of objects.
        /// </summary>
        /// <returns> The <see cref="IEnumerable{T}"/> of <see cref="TResult"/> instances. </returns>
        IEnumerable<TResult> Create();
    }

    /// <summary>
    /// Represents the factory pattern for creating a family objects.
    /// </summary>
    /// <typeparam name="TResult"> The base type of object family. </typeparam>
    /// <typeparam name="TSource"> The type of input parameter. </typeparam>
    public interface IFactory<out TResult, in TSource>
    {
        /// <summary>
        /// Creates a set of objects.
        /// </summary>
        /// <param name="source"> The input parameter for object creation. </param>
        /// <returns> The <see cref="IEnumerable{T}"/> of <see cref="TResult"/> instances. </returns>
        TResult Create(TSource source);
    }
}
