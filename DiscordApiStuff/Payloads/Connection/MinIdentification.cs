﻿using DiscordApiStuff.Payloads.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Connection
{
    public struct MinIdentification
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("intents")]
        public int Intents { get; set; }
        [JsonPropertyName("properties")]
        public Properties Property { get; set; }
        public struct Properties
        {
            [JsonPropertyName("$os")]
            public string OperatingSystem { get; set; }
            [JsonPropertyName("$browser")]
            public string Browser { get; set; }
            [JsonPropertyName("$device")]
            public string Device { get; set; }
        }
    }
}
