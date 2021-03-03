using DiscordApiStuff.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public abstract class Channel : Snowflake
    {
        [JsonPropertyName("type")]
        public ChannelType Type { get; set; }
    }
}
