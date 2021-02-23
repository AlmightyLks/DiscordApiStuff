using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Gateway
{
    public struct HeartbeatReceive
    {
        [JsonPropertyName("heartbeat_interval")]
        public int HeartbeatInterval { get; set; }
    }
}
