using System;
using System.Collections.Generic;
using System.Text;
using Chatrooms.Web.Api.Data.Entities;
using Chatrooms.Web.Api.Logic.Interfaces.Factories;
using Chatrooms.Web.Api.Models.Chat;

namespace Chatrooms.Web.Api.Logic.Factories
{
    public class ChatroomFactory : IChatroomFactory
    {
        private readonly IUserFactory _userFactory;

        public ChatroomFactory(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        public ChatroomModel Map(Chatroom chatroom) => new ChatroomModel
        {
            Id = chatroom.Id,
            Name = chatroom.Name,
            CreatedById = chatroom.CreatedById
        };

        public Chatroom Map(ChatroomModel chatroom) => new Chatroom
        {
            Id = chatroom.Id,
            Name = chatroom.Name,
            CreatedById = chatroom.CreatedById
        };

        public ChatMessageModel Map(ChatMessage message) => new ChatMessageModel
        {
            Id = message.Id,
            ChatroomId = message.ChatroomId,
            CreatedAt = message.CreatedAt,
            Text = message.Text,
            CreatedById = message.CreatedById,
            CreatedBy = message.CreatedBy != null ? _userFactory.Map(message.CreatedBy) : null
        };

        public ChatMessage Map(ChatMessageModel message) => new ChatMessage
        {
            Id = message.Id,
            ChatroomId = message.ChatroomId,
            CreatedAt = message.CreatedAt,
            Text = message.Text,
            CreatedById = message.CreatedById
        };
    }
}
