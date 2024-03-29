﻿using DiscordApiStuff.Core.Clients;
using DiscordApiStuff.Models.Classes.Message.Message;
using DiscordApiStuff.Models.Enums;
using DiscordApiStuff.Models.Interfaces;
using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Classes.Message
{
    public sealed class DiscordMessage : Snowflake, IMessage
    {
        [JsonIgnore]
        internal DiscordRestClient DiscordRestClient;

        [JsonPropertyName("channel_id")]
        public string ChannelId { get; set; }
        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; }
        [JsonPropertyName("author")]
        public DiscordUser Author { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("timestamp")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("edited_timestamp")]
        public DateTime? EditedAt { get; set; }
        [JsonPropertyName("tts")]
        public bool TextToSpeech { get; set; }
        [JsonPropertyName("mention_everyone")]
        public bool MentionsEveryone { get; set; }
        [JsonPropertyName("mentions")]
        public DiscordUser[] MentionedUsers { get; set; }
        [JsonPropertyName("mention_roles")]
        public ulong[] MentionedRoles { get; set; }
        [JsonPropertyName("mention_channels")]
        public ChannelMention[] ChannelMentions { get; set; }
        [JsonPropertyName("attachments")]
        public Attachment[] Attachments { get; set; }
        [JsonPropertyName("embeds")]
        public Embed[] Embeds { get; set; }
        [JsonPropertyName("reactions")]
        public Reaction[] Reactions { get; set; }

        //"nonce"

        [JsonPropertyName("pinned")]
        public bool Pinned { get; set; }
        [JsonPropertyName("webhook_id")]
        public ulong? WebhookId { get; set; }
        [JsonPropertyName("type")]
        public MessageType Type { get; set; }
        [JsonPropertyName("activity")]
        public MessageActivity Activity { get; set; }
        [JsonPropertyName("application")]
        public MessageApplication Application { get; set; }
        [JsonPropertyName("message_reference")]
        public MessageFlags? Flags { get; set; }
        [JsonPropertyName("stickers")]
        public Sticker[] Stickers { get; set; }
        [JsonPropertyName("referenced_message")]
        public DiscordMessage ReferencedMessage { get; set; }

        public async Task DeleteAsync()
        {
            if (DiscordRestClient == null)
            {
                throw new Exception("Non-Factory message instance cannot interact with api.");
            }
            await DiscordRestClient.DeleteMessageAsync(this);
        }
    }
}
