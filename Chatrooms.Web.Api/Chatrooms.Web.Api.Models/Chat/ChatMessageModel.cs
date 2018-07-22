using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Chatrooms.Web.Api.Models.User;

namespace Chatrooms.Web.Api.Models.Chat
{
    public class ChatMessageModel
    {
        public int Id { get; set; }

        [Required]
        public int ChatroomId { get; set; }
        
        [Required]
        public string Text { get; set; }

        public string CreatedById { get; set; }

        public DateTime CreatedAt { get; set; }

        public UserModel CreatedBy { get; set; }
    }
}
