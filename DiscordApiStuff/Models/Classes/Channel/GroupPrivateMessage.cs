using DiscordApiStuff.Converters;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public class GroupPrivateMessage : PrivateChannel
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }


        [JsonPropertyName("owner_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong OwnerId { get; set; }
    }
}
