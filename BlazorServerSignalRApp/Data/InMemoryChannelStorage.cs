namespace BlazorServerSignalRApp.Data;

public class ChannelStorage
{
    public readonly string DefaultChannelName = "Global";
    public readonly List<ChannelInfo> Channels;

    public ChannelStorage()
    {
        Channels = new(){
            new(){
                Name=DefaultChannelName,
                ConnectionIds = new()
            }
        };
    }
}

public class ChannelInfo
{
    public string Name { get; set; }
    public List<string> ConnectionIds { get; init; }
}