using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chatrooms.Web.Api.Hubs
{
    [Authorize]
    public class ChatroomHub : Hub
    {
        public const string Route = "/hubs/chatroom";
        public const string NewMessageMethod = "NewMessage";

        public static string GroupNameForRoom(int roomId) => $"Chatroom-{roomId}";

        public async Task EnterChatroom(int roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupNameForRoom(roomId));
        }

        public async Task LeaveChatroom(int roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupNameForRoom(roomId));
        }

    }
}
