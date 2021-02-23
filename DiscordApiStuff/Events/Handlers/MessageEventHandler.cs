using DiscordApiStuff.Events.EventArgs.Message;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class MessageEventHandler
    {
        public event DiscordEvent<MessageSentEventArgs> MessageSent;
        public event DiscordEvent<MessageEditedEventArgs> MessageEdited;
        public event DiscordEvent<MessageDeletedEventArgs> MessageDeleted;
        public event DiscordEvent<ReactionAddedEventArgs> ReactionAdded;
        public event DiscordEvent<ReactionRemovedEventArgs> ReactionRemoved;
        public event DiscordEvent<ReactionsClearedEventArgs> ReactionsCleared;
        public event DiscordEvent<ReactionEmojiRemovedEventArgs> ReactionEmojiRemoved;

        internal void InvokeMessageSent(ref MessageSentEventArgs ev)
            => MessageSent?.Invoke(ref ev);
        internal void InvokeMessageEdited(ref MessageEditedEventArgs ev)
            => MessageEdited?.Invoke(ref ev);
        internal void InvokeMessageDeleted(ref MessageDeletedEventArgs ev)
            => MessageDeleted?.Invoke(ref ev);
        internal void InvokeReactionAdded(ref ReactionAddedEventArgs ev)
            => ReactionAdded?.Invoke(ref ev);
        internal void InvokeReactionRemoved(ref ReactionRemovedEventArgs ev)
            => ReactionRemoved?.Invoke(ref ev);
        internal void InvokeReactionsCleared(ref ReactionsClearedEventArgs ev)
            => ReactionsCleared?.Invoke(ref ev);
        internal void InvokeReactionEmojiRemoved(ref ReactionEmojiRemovedEventArgs ev)
            => ReactionEmojiRemoved?.Invoke(ref ev);
    }
}
