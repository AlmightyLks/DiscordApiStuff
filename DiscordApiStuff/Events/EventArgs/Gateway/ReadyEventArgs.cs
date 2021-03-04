using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Models.Classes.Guild;
using System.Collections.Generic;

namespace DiscordApiStuff.Events.EventArgs.Gateway
{
    public struct ReadyEventArgs : IGatewayEventArgs
    {
        public IEnumerable<UnavailableGuild> UnavailableGuilds { get; internal set; }
    }
}
