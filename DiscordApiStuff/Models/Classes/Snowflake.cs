using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes
{
    public abstract class Snowflake
    {
        [JsonPropertyName("id")]
        public string Id { get; init; }
    }
}
