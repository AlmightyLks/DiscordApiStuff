using DiscordApiStuff.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Websocket.Connection
{
    internal struct GeneralPayloadSlim<T>
    {
        [JsonPropertyName("op")]
        public Opcode Code { get; set; }
        [JsonPropertyName("d")]
        public T Data { get; set; }
    }
}
