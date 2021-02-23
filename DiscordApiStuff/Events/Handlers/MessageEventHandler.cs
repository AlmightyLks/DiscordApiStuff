using DiscordApiStuff.Events.EventArgs.Message;
using DiscordApiStuff.Events.Interfaces;

namespace DiscordApiStuff.Events.Handlers
{
    public struct MessageEventHandler
    {
        public delegate void GuildEvent<TEvent>(TEvent ev) where TEvent : IMessageEventArgs;

        public event GuildEvent<MessageSentEventArgs> MessageSent;
        public event GuildEvent<MessageEditedEventArgs> MessageEdited;
        public event GuildEvent<MessageDeletedEventArgs> MessageDeleted;
        public event GuildEvent<ReactionAddedEventArgs> ReactionAdded;
        public event GuildEvent<ReactionRemovedEventArgs> ReactionRemoved;
        public event GuildEvent<ReactionsRemovedEventArgs> ReactionsRemoved;
        public event GuildEvent<ReactionEmojiRemovedEventArgs> ReactionEmojiRemoved;
    }
}
