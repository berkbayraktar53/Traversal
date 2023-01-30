using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using CoreLayer.EntityLayer.Abstract;

namespace CoreLayer.DataAccessLayer.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T Get(Expression<Func<T, bool>> filter);
        List<T> GetList(Expression<Func<T, bool>> filter = null);
    }
}