using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Guild
{
    public class UnavailableGuild : Snowflake
    {
        [JsonPropertyName("unavailable")]
        public bool Unavailable { get; set; }
    }
}
