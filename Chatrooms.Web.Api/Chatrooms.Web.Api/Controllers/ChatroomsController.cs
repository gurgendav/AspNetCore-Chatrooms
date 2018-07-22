using System.Threading.Tasks;
using Chatrooms.Web.Api.Helpers;
using Chatrooms.Web.Api.Hubs;
using Chatrooms.Web.Api.Logic.Interfaces;
using Chatrooms.Web.Api.Models.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Chatrooms.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatroomsController : ControllerBase
    {
        private readonly IChatroomsLogic _chatroomsLogic;
        private readonly IHubContext<ChatroomHub> _hubContext;

        public ChatroomsController(IChatroomsLogic chatroomsLogic, IHubContext<ChatroomHub> hubContext)
        {
            _chatroomsLogic = chatroomsLogic;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var chatrooms = await _chatroomsLogic.GetChatroomsListAsync();
            return Ok(chatrooms);
        }

        [HttpGet("{id:int}", Name = nameof(GetRoom))]
        public async Task<IActionResult> GetRoom(int id)
        {
            var chatroom = await _chatroomsLogic.FindChatroomAsync(id);

            if (chatroom == null)
            {
                return NotFound();
            }

            return Ok(chatroom);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRoom(ChatroomModel model)
        {
            var chatroom = await _chatroomsLogic.CreateRoomAsync(User.GetUserId(), model);
            return CreatedAtAction(nameof(GetRoom), new {id = chatroom.Id}, chatroom);
        }

        [HttpPost("{id}/messages")]
        public async Task<IActionResult> WriteMessage(int id, ChatMessageModel model)
        {
            var message = await _chatroomsLogic.WriteMessageAsync(id, User.GetUserId(), model);

            await _hubContext.Clients.Group(ChatroomHub.GroupNameForRoom(id))
                .SendCoreAsync(ChatroomHub.NewMessageMethod, new object[] {message});

            return Accepted(message);
        }

    }
}