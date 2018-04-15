using Silent.Practices.EventStore;
using Silent.Practices.Persistance;
using System.Collections.Generic;

namespace Silent.Practices.DDD
{
    public interface IRepository<TEntity, in TKey> where TEntity : IEntity<TKey>
    {
        TEntity GetById(TKey id);

        ICollection<TEntity> Get();

        bool Add(TEntity item);

        bool Update(TKey key, TEntity entity);

        bool Delete(TKey key);
    }

    public interface IRepository<TItem> : IRepository<TItem, uint> where TItem : IEntity<uint>
    {
    }

    public interface IEventAggregateRepository<TEntity, in TKey, TEventBase> : IRepository<TEntity, TKey> 
        where TEntity : EventAggregate<TKey, TEventBase>
    {
    }

    public interface IEventAggregateRepository<TEntity> : IRepository<TEntity>
        where TEntity : EventAggregate<uint, Event<uint>>
    {
    }
}