using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Entities;

using MongoDB.Driver;

using System.Collections.Generic;

namespace HuckHack.Repositories.Mongo
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(MongoContext context) : base(context, $"{nameof(Team)}s")
        {
        }

        protected override UpdateDefinition<Team> GetUpdateDefinition(Team entity)
        {
            return Builders<Team>.Update
                .Set(i => i.UserIds, entity.UserIds);
        }

        public IEnumerable<Team> GetUserTeams(string userId)
        {
            var filter = Builders<Team>.Filter.Where(i => i.UserIds.Contains(userId));
            return Collection.Find(filter).ToList();
        }
    }
}
