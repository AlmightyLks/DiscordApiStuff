using DiscordApiStuff.Converters;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public class TextChannel : DiscordChannel
    {

        [JsonPropertyName("last_message_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong? LastMessageId { get; set; }
    }
}
