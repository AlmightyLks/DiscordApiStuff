using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes
{
    public class Presence
    {
        [JsonPropertyName("activities")]
        public Activity[] Activities { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("since")]
        public int? Since { get; set; }
        [JsonPropertyName("afk")]
        public bool Afk { get; set; }
    }
}
