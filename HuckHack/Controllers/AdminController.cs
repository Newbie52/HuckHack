using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Entities;
using HuckHack.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HuckHack.Controllers
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class AdminController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public AdminController(
            IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            return View("Index");
        }

        [HttpPost]
        public void CreateEvent(Event eventModel)
        {
            _eventRepository.Add(eventModel);
        }
    }
}
