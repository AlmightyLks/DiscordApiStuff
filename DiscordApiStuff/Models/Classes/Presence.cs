using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes
{
    public class Presence
    {
        [JsonPropertyName("user")]
        public DiscordUser User { get; set; }

        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("activities")]
        public IEnumerable<Activity> Activities { get; set; }

        [JsonPropertyName("client_status")]
        public ClientStatus ClientStatus { get; set; }
    }
}
