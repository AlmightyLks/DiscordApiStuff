using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordApiStuff.Converters
{
    internal sealed class SnowflakeCollectionConverter : JsonConverter<ulong[]>
    {
        public override ulong[] Read(
            ref Utf8JsonReader reader, 
            Type typeToConvert, 
            JsonSerializerOptions options)
        {
            reader.Read();

            var result = new List<ulong>();

            while (reader.TokenType != JsonTokenType.EndArray)
            {
                var strValue = reader.GetString();
                result.Add(ulong.Parse(strValue));
                reader.Read();
            }

            return result.ToArray();
        }

        public override void Write(
            Utf8JsonWriter writer,
            ulong[] value, 
            JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (ulong item in value)
            {
                JsonSerializer.Serialize(writer, item, options);
            }
            writer.WriteEndArray();
        }
    }
}
