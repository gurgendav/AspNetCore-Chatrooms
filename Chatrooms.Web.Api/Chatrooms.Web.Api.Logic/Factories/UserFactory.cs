using Chatrooms.Web.Api.Logic.Interfaces.Factories;
using Chatrooms.Web.Api.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Chatrooms.Web.Api.Logic.Factories
{
    public class UserFactory : IUserFactory
    {
        public UserModel Map(IdentityUser user) => new UserModel
        {
            Id = user.Id,
            UserName = user.UserName
        };
    }
}