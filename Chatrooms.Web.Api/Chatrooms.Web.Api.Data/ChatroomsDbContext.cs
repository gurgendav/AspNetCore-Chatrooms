using System;
using Chatrooms.Web.Api.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chatrooms.Web.Api.Data
{
    public class ChatroomsDbContext : IdentityDbContext
    {
        public DbSet<Chatroom> Chatrooms { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        public ChatroomsDbContext(DbContextOptions<ChatroomsDbContext> options) : base(options)
        {
        }
    }
}
