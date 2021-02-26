using DiscordApiStuff.Models.Classes;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Events
{
    internal struct ReadyPayload
    {
        [JsonPropertyName("v")]
        public int GatewayVersion { get; set; }
        [JsonPropertyName("user")]
        public DiscordUser User { get; set; }
        [JsonPropertyName("private_channels")]
        public ulong[] PrivateChannels { get; set; }
        [JsonPropertyName("guilds")]
        public UnavailableGuild[] Guilds { get; set; }
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("shard")]
        public int[] Shards { get; set; }
        //See Application.cs
        [JsonPropertyName("application")]
        public Application Application { get; set; }
    }
}
