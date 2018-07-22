using System.Collections.Generic;
using System.Threading.Tasks;
using Chatrooms.Web.Api.Models.Chat;

namespace Chatrooms.Web.Api.Logic.Interfaces
{
    public interface IChatroomsLogic
    {
        Task<ChatroomModel> CreateRoomAsync(string userId, ChatroomModel model);
        Task<List<ChatroomModel>> GetChatroomsListAsync();
        Task<ChatMessageModel> WriteMessageAsync(int chatroomId, string userId, ChatMessageModel model);
        Task<ChatroomModel> FindChatroomAsync(int id);
    }
}