using DiscordApiStuff.Events.EventArgs.Guild;
using DiscordApiStuff.Events.Interfaces;

namespace DiscordApiStuff.Events.Handlers
{
    public struct GuildEventHandler
    {
        public delegate void GuildEvent<TEvent>(TEvent ev) where TEvent : IGuildEventArgs;

        public event GuildEvent<GuildCreatedEventArgs> GuildCreated;
        public event GuildEvent<GuildUpdatedEventArgs> GuildUpdated;
        public event GuildEvent<GuildDeletedEventArgs> GuildDeleted;
    }
}
