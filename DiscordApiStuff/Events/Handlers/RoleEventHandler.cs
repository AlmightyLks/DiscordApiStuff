using DiscordApiStuff.Events.EventArgs.Role;
using DiscordApiStuff.Events.Interfaces;

namespace DiscordApiStuff.Events.Handlers
{
    public struct RoleEventHandler
    {
        public delegate void RoleEvent<TEvent>(TEvent ev) where TEvent : IRoleEventEventArgs;


        public event RoleEvent<RoleCreatedEventArgs> RoleCreated;
        public event RoleEvent<RoleUpdatedEventArgs> RoleUpdated;
        public event RoleEvent<RoleDeletedEventArgs> RoleDeleted;
    }
}
