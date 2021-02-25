using DiscordApiStuff.Events.EventArgs.Message;

namespace DiscordApiStuff.Events.Handlers
{
    public sealed class MessageEventHandler
    {
        public event DiscordEventAsync<MessageSentEventArgs> MessageSent;
        public event DiscordEventAsync<MessageEditedEventArgs> MessageEdited;
        public event DiscordEventAsync<MessageDeletedEventArgs> MessageDeleted;
        public event DiscordEventAsync<ReactionAddedEventArgs> ReactionAdded;
        public event DiscordEventAsync<ReactionRemovedEventArgs> ReactionRemoved;
        public event DiscordEventAsync<ReactionsClearedEventArgs> ReactionsCleared;
        public event DiscordEventAsync<ReactionEmojiRemovedEventArgs> ReactionEmojiRemoved;

        internal MessageEventHandler() { }

        internal void InvokeMessageSent(MessageSentEventArgs ev)
            => MessageSent?.Invoke(ev);
        internal void InvokeMessageEdited(MessageEditedEventArgs ev)
            => MessageEdited?.Invoke(ev);
        internal void InvokeMessageDeleted(MessageDeletedEventArgs ev)
            => MessageDeleted?.Invoke(ev);
        internal void InvokeReactionAdded(ReactionAddedEventArgs ev)
            => ReactionAdded?.Invoke(ev);
        internal void InvokeReactionRemoved(ReactionRemovedEventArgs ev)
            => ReactionRemoved?.Invoke(ev);
        internal void InvokeReactionsCleared(ReactionsClearedEventArgs ev)
            => ReactionsCleared?.Invoke(ev);
        internal void InvokeReactionEmojiRemoved(ReactionEmojiRemovedEventArgs ev)
            => ReactionEmojiRemoved?.Invoke(ev);
    }
}
