using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HuckHack.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(
            IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IActionResult Index()
        {
            var model = new EventsViewModel
            {
                Items = _eventRepository.Get(),
                Cities = new List<string> { "Москва", "Санкт-Петербург","Казань", "Нижний Новгород"}
            };

            model.Items.Sort((i, j) => (i.StartDate >= j.StartDate ? -1 : 1));

            return View(model);
        }

        [Route("Event/{id}")]
        public IActionResult Index(string id)
        {
            var ev = _eventRepository.Get(id);
            ViewBag.EventId = id;
            ViewBag.EventName = ev.Name;
            return View("Event", ev);
        }
    }
}
