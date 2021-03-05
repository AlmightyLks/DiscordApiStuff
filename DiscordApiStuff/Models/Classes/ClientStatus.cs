using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Classes
{
    public sealed class ClientStatus
    {
        [JsonPropertyName("desktop")]
        public string Desktop { get; set; }

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("web")]
        public string Web { get; set; }
    }
}
