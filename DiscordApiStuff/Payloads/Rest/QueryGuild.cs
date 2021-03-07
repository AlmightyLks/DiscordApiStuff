using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordApiStuff.Payloads.Rest
{
    internal sealed class QueryGuild
    {
        [JsonPropertyName("with_counts")]
        public bool? WithCount { get; internal init; }
    }
}
