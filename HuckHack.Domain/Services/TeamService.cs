using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Contracts.Services;
using HuckHack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HuckHack.Domain.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUserRepository _userRepository;

        private readonly int _teamLimitCount = 4;

        public TeamService(
            ITeamRepository teamRepository,
            IUserRepository userRepository)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
        }

        public string Create(Team team, string captainId)
        {
            team.UserId = captainId;
            var teamId =  _teamRepository.Add(team);
             AddToTeam(team.Id, captainId);

             return teamId;
        }

        public void AddToTeam(string teamId, string userId)
        {
            var team = _teamRepository.Get(teamId);
            if (team != null && !team.UserIds.Contains(userId))
            {
                if (team.UserIds.Count < _teamLimitCount)
                    team.UserIds.Add(userId);
                else
                    throw new Exception("Team size reached limit");

                _teamRepository.Update(team);

                var user = _userRepository.Get(userId);
                user.TeamIds.Add(teamId);
                _userRepository.Update(user);
            }
        }

        public List<Team> GetUserTeams(string userId)
        {
            var teams = _teamRepository.GetUserTeams(userId).ToList();
            return teams;
        }

        public List<User> GetTeamUsers(string teamId)
        {
            var users = _userRepository.GetTeamUsers(teamId).ToList();
            return users;
        }
    }
}
