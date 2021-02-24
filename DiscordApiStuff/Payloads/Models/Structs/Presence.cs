﻿using System.Text.Json.Serialization;

namespace DiscordApiStuff.Payloads.Models.Structs
{
    public struct Presence
    {
        [JsonPropertyName("activities")]
        public Activity[] Activities { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("since")]
        public int? Since { get; set; }
        [JsonPropertyName("afk")]
        public bool Afk { get; set; }
    }
}
