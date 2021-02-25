using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Structs
{
    public struct Emoji
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("roles")]
        public ulong[] Roles { get; set; }
        [JsonPropertyName("user")]
        public DiscordUser User { get; set; }
        [JsonPropertyName("require_colons")]
        public bool RequireColons { get; set; }
        [JsonPropertyName("managed")]
        public bool Managed { get; set; }
        [JsonPropertyName("animated")]
        public bool Animated { get; set; }
    }
}
