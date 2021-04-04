using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webchat.Dal;
using Webchat.Model;

namespace Webchat.Bll
{
    public class MessageRepository : BaseRepository<Message>
    {
        public List<Message> GetAllMessages(int ChatId)
        {
            return context.Message.Where(Message => Message.ChatId == ChatId).ToList();
        }

        public Message GetLastMessage(int ChatId)
        {
            var list = context.Message.Where(Message => Message.ChatId == ChatId).ToList();
            return list.Count() > 0 ? list[list.Count() - 1] : null;
        }
    }
}
