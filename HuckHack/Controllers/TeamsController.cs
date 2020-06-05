using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Contracts.Services;
using HuckHack.Domain.Entities;
using HuckHack.Domain.Services;
using HuckHack.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HuckHack.Controllers
{
    [Authorize]
    public class TeamsController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITeamCardRepository _teamCardRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ITeamService _teamService;

        public TeamsController(
            IEventRepository eventRepository,
            ITeamCardRepository teamCardRepository,
            IUserRepository userRepository,
            ITeamRepository teamRepository,
            IApplicationRepository applicationRepository, 
            ITeamService teamService)
        {
            _eventRepository = eventRepository;
            _teamCardRepository = teamCardRepository;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _applicationRepository = applicationRepository;
            _teamService = teamService;
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {
            _teamService.Create(team, User.GetId());
            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public IActionResult JoinToTeam(string teamId)
        {
            _teamService.AddToTeam(teamId, User.GetId());
            return RedirectToAction("Application", new { id = teamId });
        }

        [HttpPost]
        public IActionResult CreateTeam(Team team)
        {
            var id = _teamService.Create(team, User.GetId());
            return RedirectToAction("Application", new { id = id });
        }

        [HttpGet]
        public IActionResult Application(string id)
        {
            //var application = _applicationRepository.Get(id);
            //return View("~/Views/Applications/Application.cshtml", application);
            var team = _teamRepository.Get(id);
            team.Users = _teamService.GetTeamUsers(id);
            return View("~/Views/Applications/Application.cshtml", team);
        }

        [HttpGet]
        public IActionResult ApplicationList(string eventId)
        {
            //var applications = _applicationRepository.GetByEventId(eventId);
            //return View("~/Views/Applications/List.cshtml", applications);
            var teams = _teamRepository.Get(i => i.EventId, eventId);
            return View("~/Views/Applications/List.cshtml", teams);
        }

        [HttpGet]
        public Team Get(string id) => _teamRepository.Get(id);
    }
}
