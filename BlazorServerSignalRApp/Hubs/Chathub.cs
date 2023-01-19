using Microsoft.AspNetCore.SignalR;
using BlazorServerSignalRApp.Data;

namespace BlazorServerSignalRApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageToChannel(string channelName, Message message) {
            await Clients.Group(channelName).SendAsync("ReceiveMessage", message);
        }

        public async Task JoinChannel(string name) {
            await Groups.AddToGroupAsync(Context.ConnectionId, name);
            if (!ChannelService.Channels.Contains(name)) ChannelService.Channels.Add(name);
        }

        public async Task QuitChannel(string name) {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, name);
        }
    }
}