using System;
using System.Collections.Generic;
using System.Text;
using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Entities;
using MongoDB.Driver;

namespace HuckHack.Repositories.Mongo
{
    public class ApplicationRepository : BaseRepository<ApplicationToFindHackers>, IApplicationRepository
    {
        public ApplicationRepository(MongoContext context) : base(context, "applications")
        {
        }

        protected override UpdateDefinition<ApplicationToFindHackers> GetUpdateDefinition(ApplicationToFindHackers entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationToFindHackers> GetByEventId(string eventId)
        { 
            return Collection.Find(a => a.EventId == eventId).ToList();
        }
    }
}
