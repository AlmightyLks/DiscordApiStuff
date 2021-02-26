using DiscordApiStuff.Models.Interfaces;
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
        [JsonIgnore]
        private IGuildMember _restMember;
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
        [JsonPropertyName("pending")]
        public bool? Pending { get; set; }
        [JsonPropertyName("permissions")]
        public string Permissions { get; set; }
        internal GuildMember(
            IGuildMember restUser,
            DiscordUser user,
            string nick,
            Role[] roles,
            DateTime joinedAt,
            bool deaf,
            bool mute,
            bool? pending,
            string permissions)
        {
            _restMember = restUser;
            User = user;
            Nick = nick;
            Roles = roles;
            JoinedAt = joinedAt;
            Deaf = deaf;
            Mute = mute;
            Pending = pending;
            Permissions = permissions;
        }
    }
}
