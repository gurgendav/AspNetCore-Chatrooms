using System.Threading.Tasks;
using Chatrooms.Web.Api.Helpers;
using Chatrooms.Web.Api.Logic.Interfaces;
using Chatrooms.Web.Api.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Chatrooms.Web.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationLogic _authenticationLogic;
        private readonly JwtTokenHelper _jwtTokenHelper;

        public AuthenticationController(IAuthenticationLogic authenticationLogic, JwtTokenHelper jwtTokenHelper)
        {
            _authenticationLogic = authenticationLogic;
            _jwtTokenHelper = jwtTokenHelper;
        }
        
        [HttpPost("token")]
        public async Task<IActionResult> CreateToken(LoginModel model)
        {
            var user = await _authenticationLogic.AuthenicateAsync(model);
            if (user == null)
            {
                return Unauthorized();
            }

            var token = _jwtTokenHelper.CreateTokenForUser(user);

            return Ok(token);
        }
    }
}