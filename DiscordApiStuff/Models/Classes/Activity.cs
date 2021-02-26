using DiscordApiStuff.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes
{
    public class Activity
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public ActivityType Type { get; set; }
        [JsonPropertyName("url")]
        public string URL { get; set; }
    }
}
