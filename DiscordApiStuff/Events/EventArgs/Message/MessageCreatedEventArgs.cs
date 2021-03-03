using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Models.Classes.Message;

namespace DiscordApiStuff.Events.EventArgs.Message
{
    public struct MessageCreatedEventArgs : IMessageEventArgs
    {
        public DiscordMessage Message { get; internal set; }
    }
}
