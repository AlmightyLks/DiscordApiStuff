using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes
{
    public sealed class Overwrite : Snowflake
    {
        [JsonPropertyName("type")]
        public OverwriteType Type { get; set; }
        [JsonPropertyName("allow")]
        public string Allow { get; set; }
        [JsonPropertyName("deny")]
        public string Deny { get; set; }

        public enum OverwriteType
        {
            Role,
            Member
        }
    }
}
