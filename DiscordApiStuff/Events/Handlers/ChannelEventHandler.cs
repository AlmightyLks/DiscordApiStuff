using DiscordApiStuff.Events.EventArgs.Channel;
using DiscordApiStuff.Events.EventArgs.Interfaces;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class ChannelEventHandler
    {
        public event DiscordEventAsync<ChannelCreatedEventArgs> ChannelCreated;
        public event DiscordEventAsync<ChannelUpdatedEventArgs> ChannelUpdated;
        public event DiscordEventAsync<ChannelDeletedEventArgs> ChannelDeleted;
        public event DiscordEventAsync<ChannelPinsUpdatedEventArgs> ChannelPinsUpdated;

        internal ChannelEventHandler() { }

        internal void InvokeChannelCreated(ChannelCreatedEventArgs ev)
            => ChannelCreated?.Invoke(ev);
        internal void InvokeChannelUpdated(ChannelUpdatedEventArgs ev)
            => ChannelUpdated?.Invoke(ev);
        internal void InvokeChannelDeleted(ChannelDeletedEventArgs ev)
            => ChannelDeleted?.Invoke(ev);
        internal void InvokeChannelPinsUpdated(ChannelPinsUpdatedEventArgs ev)
            => ChannelPinsUpdated?.Invoke(ev);
    }
}
