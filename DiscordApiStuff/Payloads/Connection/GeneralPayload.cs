using DiscordApiStuff.Payloads.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Connection
{
    internal struct GeneralPayload<T>
    {
        [JsonPropertyName("op")]
        public Opcode Code { get; set; }
        [JsonPropertyName("d")]
        public T Data { get; set; }
        [JsonPropertyName("s")]
        public int? Sequence { get; set; }
        [JsonPropertyName("t")]
        public string Event { get; set; }
    }
}
