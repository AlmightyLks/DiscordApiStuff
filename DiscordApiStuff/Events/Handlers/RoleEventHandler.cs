using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Events.EventArgs.Role;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class RoleEventHandler
    {
        public event DiscordEvent<RoleCreatedEventArgs> RoleCreated;
        public event DiscordEvent<RoleUpdatedEventArgs> RoleUpdated;
        public event DiscordEvent<RoleDeletedEventArgs> RoleDeleted;

        internal void InvokeRoleCreated(ref RoleCreatedEventArgs ev)
            => RoleCreated?.Invoke(ref ev);
        internal void InvokeRoleUpdated(ref RoleUpdatedEventArgs ev)
            => RoleUpdated?.Invoke(ref ev);
        internal void InvokeRoleDeleted(ref RoleDeletedEventArgs ev)
            => RoleDeleted?.Invoke(ref ev);
    }
}
