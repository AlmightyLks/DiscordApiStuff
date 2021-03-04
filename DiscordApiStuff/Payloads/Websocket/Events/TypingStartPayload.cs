using DiscordApiStuff.Models.Classes.Guild;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Websocket.Events
{
    internal struct TypingStartPayload
    {
        [JsonPropertyName("channel_id")]
        public ulong ChannelId { get; set; }
        [JsonPropertyName("guild_id")]
        public ulong? GuildId { get; set; }
        [JsonPropertyName("user_id")]
        public ulong UserId { get; set; }
        [JsonPropertyName("timestamp")]
        public int UnixTime { get; set; }
        [JsonPropertyName("channel_id")]
        public GuildMember Member { get; set; }
    }
}
