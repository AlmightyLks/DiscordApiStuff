using DiscordApiStuff.Events.EventArgs.Guild;

namespace DiscordApiStuff.Events.Handlers
{
    internal sealed class GuildEventHandler
    {
        public event DiscordEventAsync<GuildCreatedEventArgs> GuildCreated;
        public event DiscordEventAsync<GuildUpdatedEventArgs> GuildUpdated;
        public event DiscordEventAsync<GuildDeletedEventArgs> GuildDeleted;

        internal void InvokeGuildCreated(GuildCreatedEventArgs ev)
            => GuildCreated?.Invoke(ev);
        internal void InvokeGuildUpdated(GuildUpdatedEventArgs ev)
            => GuildUpdated?.Invoke(ev);
        internal void InvokeGuildDeleted(GuildDeletedEventArgs ev)
            => GuildDeleted?.Invoke(ev);
    }
}
