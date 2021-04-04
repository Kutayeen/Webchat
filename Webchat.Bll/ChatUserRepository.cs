using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webchat.Model;
namespace Webchat.Bll
{
    public class ChatUserRepository : BaseRepository<ChatUser>
    {
        public List<ChatUser> GetByUser(int UserId)
        {
            return context.ChatUser.Where(Chat => Chat.UserId == UserId).ToList();
        }
    }
}
