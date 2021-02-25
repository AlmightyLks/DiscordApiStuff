using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Structs
{
    public struct GuildMember
    {
        [JsonPropertyName("user")]
        public DiscordUser User { get; set; }
        [JsonPropertyName("nick")]
        public string Nick { get; set; }
        [JsonPropertyName("roles")]
        public Role[] Roles { get; set; }
        [JsonPropertyName("joined_at")]
        public DateTime JoinedAt { get; set; }
        [JsonPropertyName("deaf")]
        public bool Deaf { get; set; }
        [JsonPropertyName("mute")]
        public bool Mute { get; set; }
    }
}
