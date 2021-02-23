using DiscordApiStuff.Events.Handlers;

namespace DiscordApiStuff.Clients
{
    public class DiscordSocketClient
    {
        private readonly GuildEventHandler GuildEvents;
        
        private readonly ChannelEventHandler ChannelEvents;
        
        private readonly MemberEventHandler MemberEvents;
        
        private readonly MessageEventHandler MessageEvents;
        
        private readonly RoleEventHandler RoleEvents;
    }
}