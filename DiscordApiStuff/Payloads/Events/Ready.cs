using DiscordApiStuff.Payloads.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Events
{
    public struct Ready
    {
        [JsonPropertyName("v")]
        public int GatewayVersion { get; set; }
        [JsonPropertyName("user")]
        public User User { get; set; }
        [JsonPropertyName("private_channels")]
        public ulong[] PrivateChannels { get; set; }
        [JsonPropertyName("guilds")]
        public UnavailableGuild[] Guilds { get; set; }
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        [JsonPropertyName("shard")]
        public int[] Shards { get; set; }
        //See Application.cs
        [JsonPropertyName("application")]
        public object Application { get; set; }
    }
}
