using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Rest
{
    internal struct QueryGuildMembers
    {
        [JsonPropertyName("limit")]
        public int Limit { get; set; }
        [JsonPropertyName("after")]
        public string HighestUserId { get; set; }
    }
}
