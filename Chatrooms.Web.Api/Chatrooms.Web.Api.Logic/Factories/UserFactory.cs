using Chatrooms.Web.Api.Data.Entities;
using Chatrooms.Web.Api.Logic.Interfaces.Factories;
using Chatrooms.Web.Api.Models.User;

namespace Chatrooms.Web.Api.Logic.Factories
{
    public class UserFactory : IUserFactory
    {
        public UserModel Map(User user) => new UserModel
        {
            Id = user.Id,
            UserName = user.UserName
        };
    }
}