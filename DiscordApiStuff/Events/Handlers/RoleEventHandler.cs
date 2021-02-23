using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Events.EventArgs.Role;

namespace DiscordApiStuff.Events.Handlers
{
    public struct RoleEventHandler
    {
        public delegate void RoleEvent<TEvent>(TEvent ev) where TEvent : IRoleEventArgsEventArgsArgs;


        public event RoleEvent<RoleCreatedEventArgsArgsArgs> RoleCreated;
        public event RoleEvent<RoleUpdatedEventArgsArgsArgs> RoleUpdated;
        public event RoleEvent<RoleDeletedEventArgsArgsArgs> RoleDeleted;
    }
}
