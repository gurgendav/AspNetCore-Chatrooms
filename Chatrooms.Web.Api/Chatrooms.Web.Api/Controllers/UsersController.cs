using System.Threading.Tasks;
using Chatrooms.Web.Api.Logic.Interfaces;
using Chatrooms.Web.Api.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chatrooms.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersLogic _usersLogic;

        public UsersController(IUsersLogic usersLogic)
        {
            _usersLogic = usersLogic;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            var user = await _usersLogic.RegisterAsync(model);

            return Ok(user);
        }
        
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await _usersLogic.FindUserAsync(User.Identity.Name);

            return Ok(user);
        }
    }
}