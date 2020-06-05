using HuckHack.Domain.Contracts;
using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Entities;
using MongoDB.Driver;

namespace HuckHack.Repositories.Mongo
{
    public class TeamCardRepository : BaseRepository<TeamCard>, ITeamCardRepository
    {
        public TeamCardRepository(MongoContext context) : base(context, $"{nameof(TeamCard)}s")
        {
        }

        protected override UpdateDefinition<TeamCard> GetUpdateDefinition(TeamCard entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
