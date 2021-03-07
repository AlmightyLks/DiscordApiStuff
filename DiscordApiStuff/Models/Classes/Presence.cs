using DiscordApiStuff.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes
{
    public class Presence
    {
        [JsonPropertyName("user")]
        public DiscordUser User { get; set; }

        [JsonPropertyName("guild_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong GuildId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("activities")]
        public Activity[] Activities { get; set; }

        [JsonPropertyName("client_status")]
        public ClientStatus ClientStatus { get; set; }
    }
}
