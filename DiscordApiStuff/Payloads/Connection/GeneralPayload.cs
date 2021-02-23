using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Gateway.Connection
{
    public struct GeneralPayload
    {
        [JsonPropertyName("op")]
        public Opcode Code { get; set; }
        [JsonPropertyName("d")]
        public object Data { get; set; }
        [JsonPropertyName("s")]
        public int? Sequence { get; set; }
        [JsonPropertyName("t")]
        public string Event { get; set; }
    }
}
