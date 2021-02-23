using DiscordApiStuff.Events.Interfaces;

namespace DiscordApiStuff.Events.Handlers
{
    public struct ChannelEventHandler
    {
        public delegate void GuildEvent<TEvent>(TEvent ev) where TEvent : IChannelEventArgs;


        public event GuildEvent<IChannelEventArgs> ChannelCreated;
        public event GuildEvent<IChannelEventArgs> ChannelUpdated;
        public event GuildEvent<IChannelEventArgs> ChannelDeleted;
        public event GuildEvent<IChannelEventArgs> ChannelPinsUpdated;
    }
}
