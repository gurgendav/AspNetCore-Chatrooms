using System.Threading.Tasks;
using Chatrooms.Web.Api.Models.User;

namespace Chatrooms.Web.Api.Logic.Interfaces
{
    public interface IAuthenticationLogic
    {
        Task<UserModel> AuthenicateAsync(LoginModel model);
    }
}