using Silent.Tetris.DataAccess.Abstractions;
using System.Collections.Generic;

namespace Silent.Tetris.Contracts
{
    /// <summary>
    /// Represents the data access to <see cref="TSource"/> objects.
    /// </summary>
    /// <typeparam name="TSource"> The type of objects. </typeparam>
    public interface IRepository<TSource>
    {
        /// <summary>
        /// Adds the new <see cref="TSource"/> to persistance layer.
        /// </summary>
        /// <param name="source"> The <see cref="TSource"/> instance. </param>
        void Add(TSource source);

        /// <summary>
        /// Gets the collection of <see cref="TSource"/> objects.
        /// </summary>
        /// <returns> The <see cref="ICollection{TSource}"/> instance. </returns>
        ICollection<TSource> GetAll();

        /// <summary>
        /// Loads the <see cref="TSource"/> objects from persistance layer.
        /// </summary>
        void Load();

        /// <summary>
        /// Saves the <see cref="TSource"/> objects to persistance layer.
        /// </summary>
        void Save();
    }
}
