using HuckHack.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace HuckHack.Repositories.Mongo
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoConnection").Value;
            var mongoUrl = new MongoUrl(connectionString);

            var pack = new ConventionPack { new IgnoreExtraElementsConvention(true) };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName) where T : Entity
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
