using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Entities;

using MongoDB.Driver;

using System;
using System.Collections.Generic;

namespace HuckHack.Repositories.Mongo
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(MongoContext context) : base(context, $"{nameof(Event)}s")
        {   
        }

        public string GetNameByEventId(string id)
        {
            return Collection.Find(i => i.Id == id).Project(i => i.Name).FirstOrDefault();
        }

        public IEnumerable<Event> GetOnlyUpcomingEvents()
        {
            return Collection.FindSync(e => e.EndDate > DateTime.UtcNow).ToList();
        }

        protected override UpdateDefinition<Event> GetUpdateDefinition(Event entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
