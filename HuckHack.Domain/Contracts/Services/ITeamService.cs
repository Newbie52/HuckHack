using System.Collections.Generic;
using HuckHack.Domain.Entities;

namespace HuckHack.Domain.Contracts.Services
{
    public interface ITeamService
    {
        string Create(Team team, string captainId);

        void AddToTeam(string teamId, string userId);

        List<Team> GetUserTeams(string userId);

        List<User> GetTeamUsers(string teamId);
    }
}
