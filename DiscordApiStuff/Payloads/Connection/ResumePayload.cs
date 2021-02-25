using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Connection
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
