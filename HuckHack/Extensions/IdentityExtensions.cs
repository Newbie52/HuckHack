using System.Linq;
using System.Security.Claims;

namespace HuckHack.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal principal)
        {
            return principal.Claims.FirstOrDefault(i => i.Type == ClaimTypes.Sid)?.Value;
        }
    }
}
