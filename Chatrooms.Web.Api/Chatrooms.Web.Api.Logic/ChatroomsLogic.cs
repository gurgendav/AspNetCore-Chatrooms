using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chatrooms.Web.Api.Data;
using Chatrooms.Web.Api.Data.Entities;
using Chatrooms.Web.Api.Logic.Interfaces;
using Chatrooms.Web.Api.Logic.Interfaces.Factories;
using Chatrooms.Web.Api.Models.Chat;
using Microsoft.EntityFrameworkCore;

namespace Chatrooms.Web.Api.Logic
{
    public class ChatroomsLogic : IChatroomsLogic
    {
        private readonly ChatroomsDbContext _dbContext;
        private readonly IChatroomFactory _chatroomFactory;

        public ChatroomsLogic(ChatroomsDbContext dbContext, IChatroomFactory chatroomFactory)
        {
            _dbContext = dbContext;
            _chatroomFactory = chatroomFactory;
        }

        public async Task<ChatroomModel> CreateRoomAsync(string userId, ChatroomModel model)
        {
            var entity = _chatroomFactory.Map(model);
            entity.CreatedById = userId;

            await _dbContext.Chatrooms.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return _chatroomFactory.Map(entity);
        }

        public async Task<List<ChatroomModel>> GetChatroomsListAsync()
        {
            var chatrooms = await _dbContext.Chatrooms.ToListAsync();

            return chatrooms.Select(_chatroomFactory.Map).ToList();
        }

        public async Task<ChatroomModel> FindChatroomAsync(int id)
        {
            var chatroom = await _dbContext.Chatrooms.FindAsync(id);

            return chatroom != null ? _chatroomFactory.Map(chatroom) : null;
        }

        public async Task<List<ChatMessageModel>> GetChatMessages(int roomId, int count)
        {
            var messages = await _dbContext.ChatMessages
                .Where(m => m.ChatroomId == roomId)
                .OrderByDescending(m => m.CreatedAt)
                .Take(count)
                .OrderBy(m => m.CreatedAt)
                .Include(m => m.CreatedBy)
                .ToListAsync();

            return messages.Select(_chatroomFactory.Map).ToList();
        }

        public async Task<ChatMessageModel> WriteMessageAsync(int chatroomId, string userId, ChatMessageModel model)
        {
            var chatroom = await _dbContext.Chatrooms.Include(c => c.Messages).SingleAsync(c => c.Id == chatroomId);

            var message = _chatroomFactory.Map(model);
            message.CreatedById = userId;
            message.CreatedAt = DateTime.UtcNow;

            chatroom.Messages.Add(message);

            await _dbContext.SaveChangesAsync();

            message.CreatedBy = await _dbContext.Users.SingleAsync(u => u.Id == userId);

            return _chatroomFactory.Map(message);
        }
    }
}
