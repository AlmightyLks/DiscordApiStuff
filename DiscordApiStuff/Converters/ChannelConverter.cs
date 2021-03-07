using DiscordApiStuff.Models.Classes.Channel;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Converters
{
    internal sealed class ChannelConverter : JsonConverter<DiscordChannel>
    {
        public override DiscordChannel Read(
            ref Utf8JsonReader reader, 
            Type typeToConvert, 
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(
            Utf8JsonWriter writer, 
            DiscordChannel value, 
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
