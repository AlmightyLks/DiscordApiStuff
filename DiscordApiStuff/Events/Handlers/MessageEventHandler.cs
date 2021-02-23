using DiscordApiStuff.Events.EventArgs.Interfaces;
using DiscordApiStuff.Events.EventArgs.Message;

namespace DiscordApiStuff.Events.Handlers
{
    public struct MessageEventHandler
    {
        public delegate void GuildEvent<TEvent>(TEvent ev) where TEvent : IMessageEventArgsArgs;

        public event GuildEvent<MessageSentEventArgsArgs> MessageSent;
        public event GuildEvent<MessageEditedEventArgsArgs> MessageEdited;
        public event GuildEvent<MessageDeletedEventArgsArgs> MessageDeleted;
        public event GuildEvent<ReactionAddedEventArgsArgs> ReactionAdded;
        public event GuildEvent<ReactionRemovedEventArgsArgs> ReactionRemoved;
        public event GuildEvent<ReactionsRemovedEventArgsArgs> ReactionsRemoved;
        public event GuildEvent<ReactionEmojiRemovedEventArgsArgs> ReactionEmojiRemoved;
    }
}
