﻿using DiscordApiStuff.Events.EventArgs.Member;

namespace DiscordApiStuff.Events.Handlers
{
    internal sealed class MemberEventHandler
    {
        public event DiscordEventAsync<MemberJoinedEventArgs> MemberJoined;
        public event DiscordEventAsync<MemberUpdatedEventArgs> MemberUpdated;
        public event DiscordEventAsync<MemberLeftEventArgs> MemberLeft;


        internal void InvokeMemberJoined(MemberJoinedEventArgs ev)
            => MemberJoined?.Invoke(ev);
        internal void InvokeMemberUpdated(MemberUpdatedEventArgs ev)
            => MemberUpdated?.Invoke(ev);
        internal void InvokeMemberLeft(MemberLeftEventArgs ev)
            => MemberLeft?.Invoke(ev);
    }
}
