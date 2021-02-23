using DiscordApiStuff.Events.EventArgs.Interfaces;

namespace DiscordApiStuff.Events.Handlers
{
    public struct ChannelEventHandler
    {
        public delegate void GuildEvent<TEvent>(TEvent ev) where TEvent : IChannelEventArgsArgs;


        public event GuildEvent<IChannelEventArgsArgs> ChannelCreated;
        public event GuildEvent<IChannelEventArgsArgs> ChannelUpdated;
        public event GuildEvent<IChannelEventArgsArgs> ChannelDeleted;
        public event GuildEvent<IChannelEventArgsArgs> ChannelPinsUpdated;
    }
}
