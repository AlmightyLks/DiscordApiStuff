using DiscordApiStuff.Models.Interfaces;
using System;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Guild
{
    public sealed class GuildMember : DiscordUser
    {
        [JsonIgnore]
        private IGuildMember _restMember;
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
        [JsonPropertyName("pending")]
        public bool? Pending { get; set; }
        [JsonPropertyName("permissions")]
        public string Permissions { get; set; }
    }
}
