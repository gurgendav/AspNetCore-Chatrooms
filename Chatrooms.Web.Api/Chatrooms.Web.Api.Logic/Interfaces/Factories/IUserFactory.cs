using Chatrooms.Web.Api.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Chatrooms.Web.Api.Logic.Interfaces.Factories
{
    public interface IUserFactory
    {
        UserModel Map(IdentityUser user);
    }
}