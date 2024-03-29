﻿namespace CacheMemoryExample.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
    }
}
