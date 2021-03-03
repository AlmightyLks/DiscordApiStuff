using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public abstract class TextChannel : Channel
    {
        [JsonPropertyName("last_message_id")]
        public string LastMessageId { get; set; }
    }
}
