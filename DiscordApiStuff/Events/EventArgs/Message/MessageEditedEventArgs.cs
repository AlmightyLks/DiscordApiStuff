using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Models.Classes.Message;

namespace DiscordApiStuff.Events.EventArgs.Message
{
    public struct MessageEditedEventArgs : IMessageEventArgs
    {
        public DiscordMessage Message { get; internal set; }
    }
}
