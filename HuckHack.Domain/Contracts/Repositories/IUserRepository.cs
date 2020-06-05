using System.Collections.Generic;
using HuckHack.Domain.Entities;

namespace HuckHack.Domain.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);

        bool IsRegistered(string email);

        string Create(User user);

        IEnumerable<User> GetTeamUsers(string teamId);
    }
}
