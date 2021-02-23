using DiscordApiStuff.Events.EventArgs.Member;
using DiscordApiStuff.Events.Interfaces;

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
