using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Entities;

using MongoDB.Driver;

namespace HuckHack.Repositories.Mongo
{
    public class HackerCardRepository : BaseRepository<HackerCard>, IHackerCardRepository
    {
        public HackerCardRepository(MongoContext context) : base(context, $"{nameof(HackerCard)}s")
        {
        }

        protected override UpdateDefinition<HackerCard> GetUpdateDefinition(HackerCard entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
