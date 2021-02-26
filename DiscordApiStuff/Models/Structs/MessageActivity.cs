using DiscordApiStuff.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Structs
{
    public struct MessageActivity
    {
        [JsonPropertyName("type")]
        public MessageActivityType Type { get; set; }
        [JsonPropertyName("party_id")]
        public ulong? PartyId { get; set; }
    }
}
