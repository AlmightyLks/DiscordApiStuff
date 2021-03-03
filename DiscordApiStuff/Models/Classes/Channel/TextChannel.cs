using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public class TextChannel : DiscordChannel
    {

        [JsonPropertyName("last_message_id")]
        public string LastMessageId { get; set; }
    }
}
