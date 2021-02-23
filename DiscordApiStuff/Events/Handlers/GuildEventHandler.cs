using DiscordApiStuff.Events.EventArgs.Guild;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class GuildEventHandler
    {
        public event DiscordEvent<GuildCreatedEventArgs> GuildCreated;
        public event DiscordEvent<GuildUpdatedEventArgs> GuildUpdated;
        public event DiscordEvent<GuildDeletedEventArgs> GuildDeleted;

        internal void InvokeGuildCreated(ref GuildCreatedEventArgs ev)
            => GuildCreated?.Invoke(ref ev);
        internal void InvokeGuildUpdated(ref GuildUpdatedEventArgs ev)
            => GuildUpdated?.Invoke(ref ev);
        internal void InvokeGuildDeleted(ref GuildDeletedEventArgs ev)
            => GuildDeleted?.Invoke(ref ev);
    }
}
