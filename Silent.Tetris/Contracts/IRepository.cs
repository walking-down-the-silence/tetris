using System.Collections.Generic;

namespace Silent.Tetris.Contracts
{
    public interface IRepository<TSource>
    {
        void Add(TSource source);

        ICollection<TSource> GetAll();

        void Load();

        void Save();
    }
}
