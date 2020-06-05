using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HuckHack.Repositories.Mongo
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected IMongoCollection<TEntity> Collection { get; }

        protected BaseRepository(MongoContext context, string collectionName)
        {
            Collection = context.GetCollection<TEntity>(collectionName);
        }

        public string Add(TEntity entity)
        {
            Collection.InsertOne(entity);
            return entity.Id;
        }

        public void Add(IEnumerable<TEntity> entities)
        {
            Collection.InsertMany(entities);
        }

        List<TEntity> IRepository<TEntity>.Get()
        {
            return Collection.Find(_ => true).ToList();
        }

        public TEntity Get(string id)
        {
            return Collection.Find(i => i.Id == id).FirstOrDefault();
        }

        public IEnumerable<TEntity> Get(IEnumerable<string> ids)
        {
            var filter = Builders<TEntity>.Filter.In(i => i.Id, ids);
            return Collection.Find(filter).ToList();
        }

        public IEnumerable<TEntity> Get()
        {
            return Collection.Find(_ => true).ToList();
        }

        public IEnumerable<TEntity> Get<TField>(Expression<Func<TEntity, TField>> field, TField value)
        {
            var filter = Builders<TEntity>.Filter.Eq(field, value);
            return Collection.Find(filter).ToList();
        }

        public IEnumerable<TEntity> Get<TField>(Expression<Func<TEntity, TField>> field, IEnumerable<TField> values)
        {
            var filter = Builders<TEntity>.Filter.In(field, values);
            return Collection.Find(filter).ToList();
        }

        public void Update(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(i => i.Id, entity.Id);

            var update = GetUpdateDefinition(entity);
            Collection.UpdateOne(filter, update);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            var updateModels = new List<UpdateOneModel<TEntity>>();
            foreach (var entity in entities)
            {
                var filter = Builders<TEntity>.Filter.Eq(i => i.Id, entity.Id);
                var update = GetUpdateDefinition(entity);

                var updateModel = new UpdateOneModel<TEntity>(filter, update);
                updateModels.Add(updateModel);
            }

            if (updateModels.Count > 0)
                Collection.BulkWrite(updateModels);
        }

        public void Delete(string id)
        {
            Collection.DeleteOne(i => i.Id == id);
        }

        public void Delete(IEnumerable<string> ids)
        {
            var filter = Builders<TEntity>.Filter.In(i => i.Id, ids);
            Collection.DeleteMany(filter);
        }

        protected abstract UpdateDefinition<TEntity> GetUpdateDefinition(TEntity entity);
    }
}
