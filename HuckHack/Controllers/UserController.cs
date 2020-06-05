using HuckHack.Domain.Contracts.Services;
using HuckHack.Domain.Entities;
using HuckHack.Models;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HuckHack.Controllers
{
    public class UserController : Controller
    {
        public static HttpClient Client = new HttpClient();

        private readonly IUserService _userService;

        public UserController(
            IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> SignIn(string token)
        {
            var oauth = await VerifyToken(token);
            if (!string.IsNullOrEmpty(oauth.Email))
            {
                var id = _userService.Create(new User
                {
                    Email = oauth.Email,
                    FirstName = oauth.FirstName,
                    LastName = oauth.LastName
                });

                await Login(oauth.Email, id);
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        private async Task Login(string email, string id)
        {
            var user = _userService.Get(id);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Sid, id),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

        private async Task<OAuthResponseModel> VerifyToken(string token)
        {
            var response = await Client.GetAsync($"https://oauth2.googleapis.com/tokeninfo?id_token={token}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                try
                {
                    var model = JsonConvert.DeserializeObject<OAuthResponseModel>(json);
                    return model;
                }
                catch { }
            }

            return new OAuthResponseModel();
        }
    }
}
