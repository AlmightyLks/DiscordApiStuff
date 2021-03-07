using DiscordApiStuff.Models.Enums;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public sealed class GuildTextChannel : GuildChannel
    {
        [JsonPropertyName("rate_limit_per_user")]
        public int RateLimitPerUser { get; set; }

        [JsonPropertyName("topic")]
        public string Topic { get; set; }
    }
}
