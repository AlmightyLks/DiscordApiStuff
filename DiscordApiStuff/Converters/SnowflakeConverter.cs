using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Converters
{
    internal sealed class SnowflakeConverter : JsonConverter<ulong>
    {
        public override ulong Read(
            ref Utf8JsonReader reader, 
            Type typeToConvert, 
            JsonSerializerOptions options)
        {
            var strValue = reader.GetString();
            if (ulong.TryParse(strValue, out ulong result))
            {
                return result;
            }
            else
            {
                return default;
            }
        }

        public override void Write(
            Utf8JsonWriter writer, 
            ulong value, 
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
