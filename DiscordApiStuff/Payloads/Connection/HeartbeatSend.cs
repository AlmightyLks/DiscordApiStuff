using DiscordApiStuff.Payloads.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Gateway.Connection
{
    public struct HeartbeatSend
    {
        [JsonPropertyName("op")]
        public Opcode Code { get; set; }
        [JsonPropertyName("d")]
        public int? Data { get; set; }
    }
}
