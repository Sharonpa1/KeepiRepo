using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keepi.Shared
{
    public class Chat
    {
        public string FileName { get; set; }
        public User User { get; set; }
    }
    public class ChatData
    {
        public string ChatId { get; set; }
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }

    public class ChatMessage
    {
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
