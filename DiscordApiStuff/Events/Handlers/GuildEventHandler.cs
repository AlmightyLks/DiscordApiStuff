using DiscordApiStuff.Events.EventArgs.Guild;
using DiscordApiStuff.Events.EventArgs.Interfaces;

namespace DiscordApiStuff.Events.Handlers
{
    public struct GuildEventHandler
    {
        public delegate void GuildEvent<TEvent>(TEvent ev) where TEvent : IGuildEventArgsArgs;

        public event GuildEvent<GuildCreatedEventArgsArgs> GuildCreated;
        public event GuildEvent<GuildUpdatedEventArgsArgs> GuildUpdated;
        public event GuildEvent<GuildDeletedEventArgsArgs> GuildDeleted;
    }
}
