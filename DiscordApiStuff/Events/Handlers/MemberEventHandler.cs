using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Events.EventArgs.Member;
using DiscordApiStuff.Events.Processors;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class MemberEventHandler
    {
        public event DiscordEvent<MemberJoinedEventArgs> MemberJoined;
        public event DiscordEvent<MemberUpdatedEventArgs> MemberUpdated;
        public event DiscordEvent<MemberLeftEventArgs> MemberLeft;
    }
}
