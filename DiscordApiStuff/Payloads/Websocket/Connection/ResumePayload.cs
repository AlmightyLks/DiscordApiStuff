using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Websocket.Connection
{
    internal struct ResumePayload
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("session_id")]
        public string SessionId{ get; set; }
        [JsonPropertyName("seq")]
        public int? Sequence { get; set; }
    }
}
