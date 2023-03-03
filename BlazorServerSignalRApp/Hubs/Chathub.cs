using Microsoft.AspNetCore.SignalR;
using BlazorServerSignalRApp.Data;

namespace BlazorServerSignalRApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        public ChannelStorage ChannelStorage;
        public ChatHub(ChannelStorage channelStorage)
        {
            ChannelStorage = channelStorage;
        }

        public async Task SendMessage(Message message)
        {
            var channel = ChannelStorage.Channels.FirstOrDefault(x => x.ConnectionIds.Contains(Context.ConnectionId));
            if (channel != null)
                await Clients.Group(channel.Name).SendAsync("ReceiveMessage", message);
        }

        public async Task JoinChannel(string name)
        {
            var channel = ChannelStorage.Channels.FirstOrDefault(x => x.ConnectionIds.Contains(Context.ConnectionId));
            if (channel != null) await LeaveChannel(channel.Name);

            await Groups.AddToGroupAsync(Context.ConnectionId, name);

            if (ChannelStorage.Channels.All(x => x.Name != name)) ChannelStorage.Channels.Add(new()
            {
                Name = name,
                ConnectionIds = new() { Context.ConnectionId }
            });
            else ChannelStorage.Channels.First(x => x.Name == name).ConnectionIds.Add(Context.ConnectionId);
        }

        public async Task LeaveChannel(string name)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, name);

            var channel = ChannelStorage.Channels.FirstOrDefault(x => x.Name == name);
            channel?.ConnectionIds.Remove(Context.ConnectionId);
        }
    }
}