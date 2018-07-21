using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chatrooms.Web.Api.Data.Entities
{
    public class Chatroom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string CreatedById { get; set; }

        public virtual User CreatedBy { get; set; }

        public virtual ICollection<ChatMessage> Messages { get; set; }
    }
}