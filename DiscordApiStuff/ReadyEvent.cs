using System.Text.Json.Serialization;
using DiscordApiStuff.Events.Interfaces;

namespace DiscordApiStuff
{
    public struct ReadyEvent: IDiscordEvent
    {
        [JsonPropertyName("v")]
        public int GatewayVersion { get; set; }
        
        [JsonPropertyName("user")]
        public User User { get; set; }
        
        [JsonPropertyName("private_channels")]
        public ulong[] PrivateChannels { get; set; }

        //Create unavailable guild type
        //https://discord.com/developers/docs/resources/guild#unavailable-guild-object
        [JsonPropertyName("guilds")]
        public object[] Guilds { get; set; }
        
        [JsonPropertyName("shard")]
        public int[] Shards { get; set; }
        
        //Create application object type
        //https://discord.com/developers/docs/topics/oauth2#application-object
        [JsonPropertyName("application")]
        public object Application { get; set; }
    }
}