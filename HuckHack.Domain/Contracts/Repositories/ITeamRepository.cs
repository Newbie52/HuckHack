using System.Collections.Generic;
using HuckHack.Domain.Entities;

namespace HuckHack.Domain.Contracts.Repositories
{
    public interface ITeamRepository : IRepository<Team>
    {
        IEnumerable<Team> GetUserTeams(string userId);
    }
}
