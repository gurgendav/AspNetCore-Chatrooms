using System.Security.Claims;

namespace Chatrooms.Web.Api.Helpers
{
    public static class UserIdAccessHelper
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
