using Chatrooms.Web.Api.Data.Entities;
using Chatrooms.Web.Api.Models.Chat;

namespace Chatrooms.Web.Api.Logic.Interfaces.Factories
{
    public interface IChatroomFactory
    {
        ChatroomModel Map(Chatroom chatroom);
        Chatroom Map(ChatroomModel chatroom);
        ChatMessageModel Map(ChatMessage message);
        ChatMessage Map(ChatMessageModel message);
    }
}