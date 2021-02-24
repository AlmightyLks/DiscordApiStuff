using DiscordApiStuff.Payloads.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Gateway.Connection
{
    public struct MinIdentification
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
            public Properties Properties { get; set; }
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
