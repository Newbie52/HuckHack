using HuckHack.Domain.Contracts;
using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Entities;
using MongoDB.Driver;

namespace HuckHack.Repositories.Mongo
{
    public class RequestRepository : BaseRepository<Request>, IRequestRepository
    {
        public RequestRepository(MongoContext context) : base(context, $"{nameof(Request)}s")
        {
        }

        protected override UpdateDefinition<Request> GetUpdateDefinition(Request entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
