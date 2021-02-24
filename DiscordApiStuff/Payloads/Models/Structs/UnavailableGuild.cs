using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Models.Structs
{
    public struct UnavailableGuild
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("icon")]
        public string Icon { get; set; }
        [JsonPropertyName("splash")]
        public string Splash { get; set; }
        [JsonPropertyName("discovery_splash")]
        public string DiscoverySplash { get; set; }
        [JsonPropertyName("emojis")]
        public ICollection<Emoji> Emojis { get; set; }
        [JsonPropertyName("features")]
        public string[] Features { get; set; }
        [JsonPropertyName("approximate_member_count")]
        public int ApproximateMembers { get; set; }
        [JsonPropertyName("approximate_presence_count")]
        public int ApproximateMembersOnline { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
