using DiscordApiStuff.Models.Classes.Channel;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Converters
{
    internal sealed class GuildChannelConverter : JsonConverter<GuildChannel>
    {
        public override GuildChannel Read(
            ref Utf8JsonReader reader, 
            Type typeToConvert, 
            JsonSerializerOptions options)
        {
            var smth = reader.GetString();
            throw new NotImplementedException();
        }

        public override void Write(
            Utf8JsonWriter writer,
            GuildChannel value, 
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
