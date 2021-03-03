using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public class GuildVoiceChannel : GuildChannel
    {
        internal GuildVoiceChannel() { }

        [JsonPropertyName("bitrate")]
        public int Bitrate { get; set; }

        [JsonPropertyName("user_limit")]
        public byte UserLimit { get; set; }
    }
}
