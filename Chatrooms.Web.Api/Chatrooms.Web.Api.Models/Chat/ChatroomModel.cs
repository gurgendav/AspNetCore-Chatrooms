using System;
using System.Collections.Generic;
using System.Text;

namespace Chatrooms.Web.Api.Models.Chat
{
    public class ChatroomModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string CreatedById { get; set; }
    }
}
