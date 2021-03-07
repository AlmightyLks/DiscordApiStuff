using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Classes.Guild
{
    public class GuildPreview : Snowflake
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("splash")]
        public string Splash { get; set; }

        [JsonPropertyName("discovery_splash")]
        public string DiscoverySplash { get; set; }

        [JsonPropertyName("emojis")]
        public Emoji[] Emojis { get; set; }

        [JsonPropertyName("features")]
        public List<string> Features { get; set; }

        [JsonPropertyName("approximate_member_count")]
        public int ApproximateMemberCount { get; set; }

        [JsonPropertyName("approximate_presence_count")]
        public int ApproximatePresenceCount { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
