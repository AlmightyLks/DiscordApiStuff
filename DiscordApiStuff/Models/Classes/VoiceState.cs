using DiscordApiStuff.Converters;
using DiscordApiStuff.Models.Classes.Guild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Classes
{
    public sealed class VoiceState
    {
        [JsonPropertyName("guild_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong GuildId { get; set; }

        [JsonPropertyName("channel_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong ChannelId { get; set; }

        [JsonPropertyName("user_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong UserId { get; set; }

        [JsonPropertyName("member")]
        public GuildMember Member { get; set; }

        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }

        [JsonPropertyName("deaf")]
        public bool Deaf { get; set; }

        [JsonPropertyName("mute")]
        public bool Mute { get; set; }

        [JsonPropertyName("self_deaf")]
        public bool SelfDeaf { get; set; }

        [JsonPropertyName("self_mute")]
        public bool SelfMute { get; set; }

        [JsonPropertyName("suppress")]
        public bool Suppress { get; set; }

        [JsonPropertyName("self_stream")]
        public bool? SelfStream { get; set; }
    }
}
