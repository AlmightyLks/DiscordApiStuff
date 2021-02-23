using DiscordApiStuff.Events.EventArgs.Channel;
using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Events.Processors;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class ChannelEventHandler
    {
        public event DiscordEvent<ChannelCreatedEventArgs> ChannelCreated;
        public event DiscordEvent<ChannelUpdatedEventArgs> ChannelUpdated;
        public event DiscordEvent<ChannelDeletedEventArgs> ChannelDeleted;
        public event DiscordEvent<ChannelPinsUpdatedEventArgs> ChannelPinsUpdated;
    }
}
