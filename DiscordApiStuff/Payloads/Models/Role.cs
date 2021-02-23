using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Models
{
    public struct Role
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("color")]
        public int Color { get; set; }
        [JsonPropertyName("hoist")]
        public bool Hoist { get; set; }
        [JsonPropertyName("position")]
        public int Position { get; set; }
        [JsonPropertyName("permissions")]
        public string Permissions { get; set; }
        [JsonPropertyName("managed")]
        public bool Managed { get; set; }
        [JsonPropertyName("mentionable")]
        public bool Mentionable { get; set; }
    }
}
