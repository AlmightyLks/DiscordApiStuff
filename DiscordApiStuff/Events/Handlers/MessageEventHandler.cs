using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Events.EventArgs.Message;
using DiscordApiStuff.Events.Processors;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class MessageEventHandler
    {
        public event DiscordEvent<MessageSentEventArgs> MessageSent;
        public event DiscordEvent<MessageEditedEventArgs> MessageEdited;
        public event DiscordEvent<MessageDeletedEventArgs> MessageDeleted;
        public event DiscordEvent<ReactionAddedEventArgs> ReactionAdded;
        public event DiscordEvent<ReactionRemovedEventArgs> ReactionRemoved;
        public event DiscordEvent<ReactionsRemovedEventArgs> ReactionsRemoved;
        public event DiscordEvent<ReactionEmojiRemovedEventArgs> ReactionEmojiRemoved;
    }
}
