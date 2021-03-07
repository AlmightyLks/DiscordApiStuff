using DiscordApiStuff.Core.Caching;
using DiscordApiStuff.Core.Clients;
using DiscordApiStuff.Models.Classes.Message;
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
        [JsonIgnore]
        internal AppendOnlyFixedCache<DiscordMessage> MessageCache;
        
        [JsonPropertyName("type")]
        public ChannelType Type { get; set; }

        public Task<DiscordChannel> DeleteAsync()
        {
            throw new System.NotImplementedException();
        }

        //public async Task<DiscordChannel> DeleteAsync()
        //{
        //    return await DiscordRestClient.DeleteChannelAsync(ulong.Parse(Id));
        //}
    }
}
