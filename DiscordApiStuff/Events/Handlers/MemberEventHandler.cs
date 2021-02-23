using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Events.EventArgs.Member;

namespace DiscordApiStuff.Events.Handlers
{
    public struct MemberEventHandler
    {
        public delegate void MemberEvent<TEvent>(TEvent ev) where TEvent : IMemberEventArgs;

        public event MemberEvent<MemberJoinedEventArgs> MemberJoined;
        public event MemberEvent<MemberUpdatedEventArgs> MemberUpdated;
        public event MemberEvent<MemberLeftEventArgs> MemberLeft;
    }
}
