using DiscordApiStuff.Models.Enums;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes
{
    public class DiscordUser : Snowflake
    {

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("discriminator")]
        public string Discriminator { get; set; }

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }

        [JsonPropertyName("bot")]
        public bool? IsBot { get; set; }

        [JsonPropertyName("system")]
        public bool? IsDiscordUser { get; set; }

        [JsonPropertyName("mfa_enabled")]
        public bool? MultiFactorAuthentication { get; set; }

        [JsonPropertyName("locale")]
        public string LocalLanguage { get; set; }

        [JsonPropertyName("verified")]
        public bool? EMailVerified { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("flags")]
        public UserFlags? Flags { get; set; }

        [JsonPropertyName("premium_type")]
        public Nitro Nitro { get; set; }

        [JsonPropertyName("public_flags")]
        public UserFlags? PublicFlags { get; set; }
    }
}