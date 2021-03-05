using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Models.Classes.Guild;

namespace DiscordApiStuff.Events.EventArgs.Guild
{
    public struct GuildCreatedEventArgs : IGuildEventArgs
    {
        public DiscordGuild Guild { get; internal init; }
    }
}
