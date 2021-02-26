using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes
{
    public class Reaction
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
        [JsonPropertyName("me")]
        public bool HasReacted { get; set; }
        [JsonPropertyName("emoji")]
        public Emoji Emoji { get; set; }
    }
}
