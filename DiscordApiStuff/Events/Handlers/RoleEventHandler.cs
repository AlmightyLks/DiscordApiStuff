using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Events.EventArgs.Role;
using DiscordApiStuff.Events.Processors;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class RoleEventHandler
    {
        public event DiscordEvent<RoleCreatedEventArgs> RoleCreated;
        public event DiscordEvent<RoleUpdatedEventArgs> RoleUpdated;
        public event DiscordEvent<RoleDeletedEventArgs> RoleDeleted;
    }
}
