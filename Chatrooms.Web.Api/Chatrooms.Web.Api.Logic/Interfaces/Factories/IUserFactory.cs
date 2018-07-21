using Chatrooms.Web.Api.Data.Entities;
using Chatrooms.Web.Api.Models.User;

namespace Chatrooms.Web.Api.Logic.Interfaces.Factories
{
    public interface IUserFactory
    {
        UserModel Map(User user);
    }
}