using DiscordApiStuff.Models.Classes.Channel;
using DiscordApiStuff.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Converters
{
    internal sealed class GuildChannelCollectionConverter : JsonConverter<GuildChannel[]>
    {
        public override GuildChannel[] Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            var result = new List<GuildChannel>();

            reader.Read();

            /*
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                reader.Read();
                GuildChannel channel = new GuildChannel();

                channel.Id = ulong.Parse(reader.GetString());
                reader.Read();
                channel.Type = (ChannelType)reader.GetByte();
                reader.Read();

                while (reader.TokenType != JsonTokenType.EndObject)
                {
                    // Get the key.
                    if (reader.TokenType != JsonTokenType.PropertyName)
                    {

                    }

                    string propertyName = reader.GetString();
                    switch (channel.Type)
                    {
                        case ChannelType.GuildText:
                            {
                                switch (propertyName)
                                {
                                    case "":
                                        break;
                                }
                                break;
                            }
                        case ChannelType.GuildVoice:
                            {

                                break;
                            }
                        case ChannelType.GuildCategory:
                            {

                                break;
                            }
                    }
                    // For performance, parse with ignoreCase:false first.
                    if (!Enum.TryParse(propertyName, ignoreCase: false, out TKey key) &&
                        !Enum.TryParse(propertyName, ignoreCase: true, out key))
                    {
                        throw new JsonException(
                            $"Unable to convert \"{propertyName}\" to Enum \"{_keyType}\".");
                    }

                    // Get the value.
                    TValue value;
                    if (_valueConverter != null)
                    {
                        reader.Read();
                        value = _valueConverter.Read(ref reader, _valueType, options);
                    }
                    else
                    {
                        value = JsonSerializer.Deserialize<TValue>(ref reader, options);
                    }

                    // Add to dictionary.
                    dictionary.Add(key, value);
                }
                reader.Read();
            }
            */
            return result.ToArray();
        }

        public override void Write(
            Utf8JsonWriter writer,
            GuildChannel[] value,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
