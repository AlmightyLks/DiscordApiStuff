using DiscordApiStuff.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public class GuildChannel : DiscordChannel
    {
        [JsonPropertyName("guild_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong GuildId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("permission_overwrites")]
        public List<object> PermissionOverwrites { get; set; }

        [JsonPropertyName("parent_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong CategoryId { get; set; }

        [JsonPropertyName("nsfw")]
        public bool Nsfw { get; set; }
    }
}
