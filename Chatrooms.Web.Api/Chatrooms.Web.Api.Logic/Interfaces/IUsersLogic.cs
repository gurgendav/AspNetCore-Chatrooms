using System.Threading.Tasks;
using Chatrooms.Web.Api.Models.User;

namespace Chatrooms.Web.Api.Logic.Interfaces
{
    public interface IUsersLogic
    {
        Task<UserModel> RegisterAsync(RegisterModel model);
        Task<UserModel> FindUserAsync(string userName);
    }
}