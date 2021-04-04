using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webchat.Bll;
using Webchat.Model;

namespace Webchat.Hubs
{
    public class SignalrHub : Hub
    {
        public async Task SendMessage(string user, string userId, string chatId, string message)
        {
            MessageRepository messageRepository = new MessageRepository();
            messageRepository.Add(new Message { ChatId= Int32.Parse(chatId), Sender= Int32.Parse(userId), Content = message, SendTime = DateTime.Now});
            await Clients.All.SendAsync("ReceiveMessage", user, userId, chatId, message);
        }
    }
}
