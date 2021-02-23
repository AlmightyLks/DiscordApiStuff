using DiscordApiStuff.Events.EventArgs.Guild;
using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Events.Processors;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class GuildEventHandler
    {
        public event DiscordEvent<GuildCreatedEventArgs> GuildCreated;
        public event DiscordEvent<GuildUpdatedEventArgs> GuildUpdated;
        public event DiscordEvent<GuildDeletedEventArgs> GuildDeleted;
    }
}
