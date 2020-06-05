using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Contracts.Services;
using HuckHack.Extensions;
using HuckHack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HuckHack.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeamService _teamService;

        public ProfileController(
            IUserRepository userRepository,
            ITeamService teamService)
        {
            _userRepository = userRepository;
            _teamService = teamService;
        }

        [HttpGet]
        [Route("[controller]/{id}")]
        [Route("[controller]")]
        public IActionResult Index(string id)
        {
            var owner = IsOwner(id);
            var user = owner ? _userRepository.GetByEmail(User.Identity.Name) : _userRepository.Get(id);
            var profile = new ProfileViewModel
            {
                Id = user.Id,
                Owner = owner,
                Picture = user.Picture,
                FirstName = user.FirstName,
                City = user.City,
                Age = user.Age,
                HardSkills = user.HardSkills,
                SoftSkills = user.SoftSkills,
                LastName = user.LastName,
                HackathonsExperience = user.HackathonsExperience,
                PortfolioLinks = user.PortfolioLinks,
                Specialty = user.Specialty,
                VK = user.Links.VK,
                Facebook = user.Links.Facebook,
                TG = user.Links.TG,
                Github = user.Links.Github,
            };

            if (owner)
                ViewBag.Teams = _teamService.GetUserTeams(User.GetId());
            else
                ViewBag.GuestTeams = _teamService.GetUserTeams(User.GetId());

            return View(profile);
        }

        [HttpPost]
        public IActionResult Index(ProfileViewModel model)
        {
            var user = _userRepository.GetByEmail(User.Identity.Name);
            user.FirstName = model.FirstName;
            user.Specialty = model.Specialty;
            user.LastName = model.LastName;
            user.Age = model.Age;
            user.City = model.City;
            user.SoftSkills = model.SoftSkills;
            user.HardSkills = model.HardSkills;
            user.PortfolioLinks = model.PortfolioLinks;
            user.HackathonsExperience = model.HackathonsExperience;
            user.Links.VK = model.VK;
            user.Links.Github = model.Github;
            user.Links.TG = model.TG;
            user.Links.Facebook = model.Facebook;
            _userRepository.Update(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SetPicture(IFormFile imgFile)
        {
            var user = _userRepository.GetByEmail(User.Identity.Name);
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                imgFile.OpenReadStream().CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            user.Picture = Convert.ToBase64String(bytes);
            _userRepository.Update(user);
            return RedirectToAction("Index");
        }

        private bool IsOwner(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return true;

            return User.GetId() == id;
        }
    }
}
