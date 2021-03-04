using DiscordApiStuff.Models.Classes;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Websocket.Connection
{
    internal struct Identification
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("intents")]
        public int Intents { get; set; }
        [JsonPropertyName("properties")]
        public Properties Property { get; set; }
        [JsonPropertyName("compress")]
        public bool? Compressed { get; set; }
        [JsonPropertyName("large_threshold")]
        public int? LargeThreshold { get; set; }
        [JsonPropertyName("shard")]
        public int[] Shard { get; set; }
        [JsonPropertyName("presence")]
        public Presence? Presence { get; set; }
        public struct Properties
        {
            [JsonPropertyName("$os")]
            public string OperatingSystem { get; set; }
            [JsonPropertyName("$browser")]
            public string Browser { get; set; }
            [JsonPropertyName("$device")]
            public string Device { get; set; }
        }
    }
}
