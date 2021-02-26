using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Classes
{
    public class MessageReference
    {
        [JsonPropertyName("message_id")]
        public ulong? MessageId { get; set; }
        [JsonPropertyName("channel_id")]
        public ulong? ChannelId { get; set; }
        [JsonPropertyName("guild_id")]
        public ulong? GuildId { get; set; }
        [JsonPropertyName("fail_if_not_exists")]
        public bool ErrorWhenNotExisting { get; set; }
    }
}
