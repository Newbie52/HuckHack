using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HuckHack.Domain.Entities;

namespace HuckHack.Domain.Contracts.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        string Add(TEntity entity);

        void Add(IEnumerable<TEntity> entities);

        List<TEntity> Get();

        TEntity Get(string id);

        IEnumerable<TEntity> Get(IEnumerable<string> ids);

        IEnumerable<TEntity> Get<TField>(Expression<Func<TEntity, TField>> field, TField value);

        IEnumerable<TEntity> Get<TField>(Expression<Func<TEntity, TField>> field, IEnumerable<TField> values);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Delete(string id);

        void Delete(IEnumerable<string> ids);
    }
}
