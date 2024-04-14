using CS.DOM.DTO;
using Microsoft.AspNetCore.SignalR;

namespace CS.BL.Hubs;

public class DialogHub : Hub
{
    public async Task SendMessage(ChatMessageDto message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message.Text, message.UserName);
    }
}