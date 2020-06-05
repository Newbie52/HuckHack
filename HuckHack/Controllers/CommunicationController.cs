using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HuckHack.Controllers
{
    [Authorize]
    public class CommunicationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICommunicationService _communicationService;

        public CommunicationController(
            IUserRepository userRepository, 
            ICommunicationService communicationService)
        {
            _userRepository = userRepository;
            _communicationService = communicationService;
        }

        public async Task<IActionResult> SendInviteRequest(InviteRequestViewModel model)
        {
            var userFrom = _userRepository.GetByEmail(User.Identity.Name);

            if (userFrom.SendedInviteRequests.Contains(model.To))
                return Conflict();

            userFrom.SendedInviteRequests.Add(model.To);
            _userRepository.Update(userFrom);

            var userTo = _userRepository.Get(model.To);
            //var profileLink = $"huckhack.com/Profile/{userFrom.Id}";
            var profileLink = $"http://localhost:5000/Teams/JoinToTeam?teamId={model.TeamId}";

            await _communicationService.SendInviteEmail(userFrom.ShortDisplayName, profileLink, userTo.Email, model.CoverMessage);
            return Ok();
        }
    }

    public class InviteRequestViewModel
    {
        public string To { get; set; }

        public string CoverMessage { get; set; }

        public string TeamId { get; set; }
    }
}
