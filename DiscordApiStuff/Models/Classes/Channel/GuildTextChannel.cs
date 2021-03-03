using DiscordApiStuff.Models.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public sealed class GuildTextChannel : TextChannel
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("permission_overwrites")]
        public List<object> PermissionOverwrites { get; set; }

        [JsonPropertyName("rate_limit_per_user")]
        public int RateLimitPerUser { get; set; }

        [JsonPropertyName("topic")]
        public string Topic { get; set; }
    }
}
