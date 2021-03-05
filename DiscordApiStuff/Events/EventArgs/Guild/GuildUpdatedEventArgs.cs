using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Models.Classes.Guild;

namespace DiscordApiStuff.Events.EventArgs.Guild
{
    public struct GuildUpdatedEventArgs : IGuildEventArgs
    {
        public DiscordGuild Guild { get; internal set; }
    }
}
