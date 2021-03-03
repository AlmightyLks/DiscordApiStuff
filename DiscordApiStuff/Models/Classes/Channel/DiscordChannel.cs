using DiscordApiStuff.Core.Clients;
using DiscordApiStuff.Models.Enums;
using DiscordApiStuff.Models.Interfaces;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Classes.Channel
{
    public class DiscordChannel : Snowflake, IChannel
    {
        [JsonIgnore]
        internal DiscordRestClient DiscordRestClient;

        [JsonPropertyName("type")]
        public ChannelType Type { get; set; }

        public async Task<DiscordChannel> DeleteAsync()
        {
            return await DiscordRestClient.DeleteChannelAsync(ulong.Parse(Id));
        }
    }
}
