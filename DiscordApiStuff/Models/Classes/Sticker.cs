using DiscordApiStuff.Converters;
using DiscordApiStuff.Models.Enums;
using DiscordApiStuff.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Classes
{
    public class Sticker : Snowflake
    {
        [JsonPropertyName("pack_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong PackId { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("tags")]
        public string Tags { get; set; }
        [JsonPropertyName("asset")]
        public string AssetHash { get; set; }
        [JsonPropertyName("preview_asset")]
        public string PreviewAssetHash { get; set; }
        [JsonPropertyName("format_type")]
        public StickerFormatType FormatType { get; set; }
    }
}
