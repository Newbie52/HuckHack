using System.Collections.Generic;
using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Entities;

using MongoDB.Driver;

namespace HuckHack.Repositories.Mongo
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MongoContext context) : base(context, $"{nameof(User)}s")
        {
        }

        protected override UpdateDefinition<User> GetUpdateDefinition(User entity)
        {
            return Builders<User>.Update
                .Set(u => u.FirstName, entity.FirstName)
                .Set(u => u.LastName, entity.LastName)
                .Set(u => u.City, entity.City)
                .Set(u => u.SoftSkills, entity.SoftSkills)
                .Set(u => u.HardSkills, entity.HardSkills)
                .Set(u => u.Picture, entity.Picture)
                .Set(u => u.HackathonsExperience, entity.HackathonsExperience)
                .Set(u => u.Age, entity.Age)
                .Set(u => u.Links, entity.Links)
                .Set(u => u.SendedInviteRequests, entity.SendedInviteRequests)
                .Set(u => u.TeamIds, entity.TeamIds)
                .Set(u => u.PortfolioLinks, entity.PortfolioLinks)
                .Set(u => u.Specialty, entity.Specialty);
            }

        public User GetByEmail(string email)
        {
            return Collection.Find(u => u.Email == email).FirstOrDefault();
        }

        public bool IsRegistered(string email)
        {
            var users = Collection.Find(u => u.Email == email).Limit(1).ToList();
            return users.Count == 1;
        }

        public string Create(User user) => Add(user);

        public IEnumerable<User> GetTeamUsers(string teamId)
        {
            var filter = Builders<User>.Filter.Where(i => i.TeamIds.Contains(teamId));
            return Collection.Find(filter).ToList();
        }
    }
}
