using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Contracts.Services;
using HuckHack.Domain.Entities;
using HuckHack.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HuckHack.Controllers
{
    [Authorize]
    public class HackerCardsController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHackerCardRepository _hackerCardRepository;
        private readonly IHackerCardService _hackerCardService;

        public HackerCardsController(
            IEventRepository eventRepository,
            IUserRepository userRepository,
            IHackerCardRepository hackerCardRepository,
            IHackerCardService hackerCardService)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _hackerCardRepository = hackerCardRepository;
            _hackerCardService = hackerCardService;
        }

        [Route("/Hackers")]
        [Route("/Hackers/{eventId}")]
        public IActionResult Index(string eventId)
        {
            ViewBag.User = _userRepository.GetByEmail(User.Identity.Name);
            var events = _eventRepository.GetOnlyUpcomingEvents();
            ViewBag.Events = events;
            var hackerCards = new List<HackerCard>();
            if (!string.IsNullOrEmpty(eventId))
            {
                hackerCards = _hackerCardRepository.Get(c => c.EventId, eventId).ToList();
                ViewBag.EventName = events.FirstOrDefault(e => e.Id == eventId)?.Name;
                ViewBag.EventId = eventId;
            }

            return View(new HackerCardPageModel { Hackers = hackerCards });
        }

        [HttpPost]
        public IActionResult Create(CreateHackerCard model)
        {
            var user = _userRepository.GetByEmail(User.Identity.Name);
            var card = new HackerCard
            {
                Picture = user.Picture,
                Age = user.Age,
                City = user.City,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.Id,
                Email = user.Email,
                Specialty = user.Specialty,
                Skills = user.HardSkills,
                Description = model.Description,
                EventId = model.EventId
            };
            _hackerCardService.Create(User.Identity.Name, card);
            return RedirectToAction("Index", "HackerCards", new { id = model.EventId });
        }

        [HttpPost]
        [Route("Hackers/{eventId}")]
        public IActionResult Search([FromForm] HackerCardPageModel model, [FromRoute] string eventId)
        {
            var hackers = _hackerCardService.Search(model.Filter, eventId);
            ViewBag.EventId = eventId;
            return View("Index", new HackerCardPageModel { Hackers = hackers });
        }

        [HttpGet]
        public IEnumerable<object> Specialties() => Enum.GetValues(typeof(Specialty))
            .Cast<Specialty>()
            .Select(spec => new
            {
                specialtyId = (int)spec,
                specialtyName = spec.ToString()
            });

        //private HackerCard Map(HackerRequest request)
        //{
        //    return new HackerCard
        //    {
        //        UserId = request.UserId,
        //        FirstName = request.FirstName,
        //        LastName = request.LastName,
        //        Description = request.Description,
        //        City = request.City,
        //        Email = User.Identity.Name,
        //        EventId = request.EventId,
        //        EventName = request.EventName,
        //        Experience = request.Experience,
        //        ExperienceYears = request.ExperienceYears,
        //        GitHub = request.GitHub,
        //        HackathonsCount = request.HackathonsCount,
        //        Specialty = request.Specialty,
        //        Skills = request.Skills
        //    };
        //}
    }
}
