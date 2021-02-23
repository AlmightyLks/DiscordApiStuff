using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Gateway
{
    public struct Identification
    {
        [JsonPropertyName("op")]
        public Opcode Code { get; set; }
        [JsonPropertyName("d")]
        public AllData Data { get; set; }
        public struct AllData
        {
            [JsonPropertyName("token")]
            public string Token { get; set; }
            [JsonPropertyName("intents")]
            public int Intents { get; set; }
            [JsonPropertyName("properties")]
            public Properties Property { get; set; }
            [JsonPropertyName("compress")]
            public bool? Compress { get; set; }
            [JsonPropertyName("large_threshold")]
            public int? LargeThreshold { get; set; }
            [JsonPropertyName("shard")]
            public int[] Shard { get; set; }
            [JsonPropertyName("presence")]
            public Presence? Presence { get; set; }
        }
        public struct Presence
        {
            [JsonPropertyName("activities")]
            public Activity[] Activities { get; set; }
            [JsonPropertyName("status")]
            public string Status { get; set; }
            [JsonPropertyName("since")]
            public int? Since { get; set; }
            [JsonPropertyName("afk")]
            public bool Afk { get; set; }
        }
        public struct Activity
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("type")]
            public ActivityType Type { get; set; }
            [JsonPropertyName("url")]
            public string URL { get; set; }
        }
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
