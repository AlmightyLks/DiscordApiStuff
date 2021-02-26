using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Models;

namespace DiscordApiStuff.Events.EventArgs.Message
{
    public struct MessageCreatedEventArgs : IMessageEventArgs
    {
        public Models.Classes.Message Message { get; internal set; }
    }
}
