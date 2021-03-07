using DiscordApiStuff.Converters;
using DiscordApiStuff.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes
{
    public class ChannelMention : Snowflake
    {
        [JsonPropertyName("guild_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong GuildId { get; set; }
        [JsonPropertyName("type")]
        public ChannelType Type { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
