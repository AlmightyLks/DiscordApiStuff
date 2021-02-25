using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Connection
{
    internal struct HeartbeatReceive
    {
        [JsonPropertyName("heartbeat_interval")]
        public int HeartbeatInterval { get; set; }
    }
}
