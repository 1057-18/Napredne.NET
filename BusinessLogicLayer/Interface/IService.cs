﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Interface
{
    public interface IService<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllWithOutTracking();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
