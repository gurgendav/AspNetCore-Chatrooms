﻿using System.Threading.Tasks;
using Chatrooms.Web.Api.Data.Entities;
using Chatrooms.Web.Api.Logic.Interfaces;
using Chatrooms.Web.Api.Logic.Interfaces.Factories;
using Chatrooms.Web.Api.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Chatrooms.Web.Api.Logic
{
    public class AuthenticationLogic : IAuthenticationLogic
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserFactory _userFactory;

        public AuthenticationLogic(
            SignInManager<User> signInManager, 
            UserManager<User> userManager, 
            IUserFactory userFactory)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userFactory = userFactory;
        }

        public async Task<UserModel> AuthenicateAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                return null;
            }

            return _userFactory.Map(user);
        }
    }
}
