using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public class PrivateChannel : DiscordChannel
    {
        [JsonPropertyName("last_message_id")]
        public string LastMessageId { get; set; }

        [JsonPropertyName("recipients")]
        public IEnumerable<DiscordUser> Recipients { get; set; }
    }
}
