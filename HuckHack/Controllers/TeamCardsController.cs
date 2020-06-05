using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Entities;
using HuckHack.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace HuckHack.Controllers
{
    [Authorize]
    public class TeamCardsController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly ITeamCardRepository _teamCardRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;

        public TeamCardsController(
            IEventRepository eventRepository,
            ITeamCardRepository teamCardRepository,
            IUserRepository userRepository,
            ITeamRepository teamRepository)
        {
            _eventRepository = eventRepository;
            _teamCardRepository = teamCardRepository;
            _userRepository = userRepository;
            _teamRepository = teamRepository;
        }

        public IActionResult Index(string eventId)
        {
            ViewBag.User = _userRepository.GetByEmail(User.Identity.Name);
            var events = _eventRepository.Get();
            ViewBag.Events = events;
            ViewBag.Teams = _teamRepository.Get(i => i.UserId, User.GetId());
            var teamCards = new List<TeamCard>();
            if (!string.IsNullOrEmpty(eventId))
            {
                teamCards = _teamCardRepository.Get(c => c.EventId, eventId).ToList();
                ViewBag.EventName = events.FirstOrDefault(e => e.Id == eventId)?.Name;
            }

            return View(teamCards);
        }

        [HttpPost]
        public IActionResult Create(TeamCard card)
        {
            var team = _teamRepository.Get(card.TeamId);
            if (team.UserId != User.GetId())
                return Forbid();

            card.Name = team.Name;
            card.Description = team.ShortDescription;
            card.MembersCount = team.MembersCount;
            card.Requirements = team.Requirements;
            card.UserId = User.GetId();
            _teamCardRepository.Add(card);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create() => View();
    }
}
