using DiscordApiStuff.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public class PrivateChannel : DiscordChannel
    {
        [JsonPropertyName("last_message_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong? LastMessageId { get; set; }

        [JsonPropertyName("recipients")]
        public DiscordUser[] Recipients { get; set; }
    }
}
