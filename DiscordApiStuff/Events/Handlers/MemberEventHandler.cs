using DiscordApiStuff.Events.EventArgs.Member;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class MemberEventHandler
    {
        public event DiscordEvent<MemberJoinedEventArgs> MemberJoined;
        public event DiscordEvent<MemberUpdatedEventArgs> MemberUpdated;
        public event DiscordEvent<MemberLeftEventArgs> MemberLeft;


        internal void InvokeMemberJoined(ref MemberJoinedEventArgs ev)
            => MemberJoined?.Invoke(ref ev);
        internal void InvokeMemberUpdated(ref MemberUpdatedEventArgs ev)
            => MemberUpdated?.Invoke(ref ev);
        internal void InvokeMemberLeft(ref MemberLeftEventArgs ev)
            => MemberLeft?.Invoke(ref ev);
    }
}
