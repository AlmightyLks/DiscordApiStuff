using DiscordApiStuff.Payloads.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Models.Structs
{
    public struct Activity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public ActivityType Type { get; set; }
        [JsonPropertyName("url")]
        public string URL { get; set; }
    }
}
