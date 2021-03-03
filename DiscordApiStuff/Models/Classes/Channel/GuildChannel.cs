using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public class GuildChannel : DiscordChannel
    {

        [JsonPropertyName("guild_id")]
        public string GuildId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("permission_overwrites")]
        public List<object> PermissionOverwrites { get; set; }

        [JsonPropertyName("parent_id")]
        public string CategoryId { get; set; }

        [JsonPropertyName("nsfw")]
        public bool Nsfw { get; set; }
    }
}
