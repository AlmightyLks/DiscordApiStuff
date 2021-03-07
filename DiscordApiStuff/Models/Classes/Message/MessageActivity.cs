using DiscordApiStuff.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Message
{
    public class MessageActivity
    {
        [JsonPropertyName("type")]
        public MessageActivityType Type { get; set; }
        [JsonPropertyName("party_id")]
        public string PartyId { get; set; }
    }
}
