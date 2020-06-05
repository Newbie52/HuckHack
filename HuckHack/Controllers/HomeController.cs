using HuckHack.Models;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using HuckHack.Domain.Contracts.Repositories;

namespace HuckHack.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public HomeController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
