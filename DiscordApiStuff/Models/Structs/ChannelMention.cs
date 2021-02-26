using DiscordApiStuff.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Structs
{
    public struct ChannelMention
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; }
        [JsonPropertyName("type")]
        public ChannelType Type { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
