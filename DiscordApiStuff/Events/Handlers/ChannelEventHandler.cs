using DiscordApiStuff.Events.EventArgs.Channel;
using DiscordApiStuff.Events.EventArgs.Interfaces;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class ChannelEventHandler
    {
        public event DiscordEvent<ChannelCreatedEventArgs> ChannelCreated;
        public event DiscordEvent<ChannelUpdatedEventArgs> ChannelUpdated;
        public event DiscordEvent<ChannelDeletedEventArgs> ChannelDeleted;
        public event DiscordEvent<ChannelPinsUpdatedEventArgs> ChannelPinsUpdated;

        internal void InvokeChannelCreated(ref ChannelCreatedEventArgs ev)
            => ChannelCreated?.Invoke(ref ev);
        internal void InvokeChannelUpdated(ref ChannelUpdatedEventArgs ev)
            => ChannelUpdated?.Invoke(ref ev);
        internal void InvokeChannelDeleted(ref ChannelDeletedEventArgs ev)
            => ChannelDeleted?.Invoke(ref ev);
        internal void InvokeChannelPinsUpdated(ref ChannelPinsUpdatedEventArgs ev)
            => ChannelPinsUpdated?.Invoke(ref ev);
    }
}
