using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Events.EventArgs.Role;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class RoleEventHandler
    {
        public event DiscordEventAsync<RoleCreatedEventArgs> RoleCreated;
        public event DiscordEventAsync<RoleUpdatedEventArgs> RoleUpdated;
        public event DiscordEventAsync<RoleDeletedEventArgs> RoleDeleted;

        internal void InvokeRoleCreated(RoleCreatedEventArgs ev)
            => RoleCreated?.Invoke(ev);
        internal void InvokeRoleUpdated(RoleUpdatedEventArgs ev)
            => RoleUpdated?.Invoke(ev);
        internal void InvokeRoleDeleted(RoleDeletedEventArgs ev)
            => RoleDeleted?.Invoke(ev);
    }
}
