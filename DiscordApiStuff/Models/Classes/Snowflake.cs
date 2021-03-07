using DiscordApiStuff.Converters;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes
{
    public abstract class Snowflake
    {
        [JsonPropertyName("id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong Id { get; init; }
    }
}
