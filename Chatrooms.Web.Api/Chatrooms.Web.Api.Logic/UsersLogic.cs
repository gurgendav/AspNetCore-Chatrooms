using System;
using System.Linq;
using System.Threading.Tasks;
using Chatrooms.Web.Api.Logic.Interfaces;
using Chatrooms.Web.Api.Logic.Interfaces.Factories;
using Chatrooms.Web.Api.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Chatrooms.Web.Api.Logic
{
    public class UsersLogic : IUsersLogic
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserFactory _userFactory;

        public UsersLogic(UserManager<IdentityUser> userManager, IUserFactory userFactory)
        {
            _userManager = userManager;
            _userFactory = userFactory;
        }

        public async Task<UserModel> FindUserAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return _userFactory.Map(user);
        }

        public async Task<UserModel> RegisterAsync(RegisterModel model)
        {
            var user = new IdentityUser(model.UserName);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException();
            }

            return _userFactory.Map(user);
        }
    }
}