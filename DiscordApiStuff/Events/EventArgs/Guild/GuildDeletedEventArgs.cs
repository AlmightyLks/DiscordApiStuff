using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Models.Classes.Guild;

namespace DiscordApiStuff.Events.EventArgs.Guild
{
    public struct GuildDeletedEventArgs : IGuildEventArgs
    {
        public UnavailableGuild UnavailableGuild { get; internal init; }
        public bool WasRemoved { get; internal init; }
    }
}
