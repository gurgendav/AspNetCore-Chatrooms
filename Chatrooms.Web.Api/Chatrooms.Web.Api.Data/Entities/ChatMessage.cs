using System;
using System.ComponentModel.DataAnnotations;

namespace Chatrooms.Web.Api.Data.Entities
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ChatroomId { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Chatroom Chatroom { get; set; }
    }
}
