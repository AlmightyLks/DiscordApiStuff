using DiscordApiStuff.Converters;
using DiscordApiStuff.Core.Caching;
using DiscordApiStuff.Models.Classes.Channel;
using DiscordApiStuff.Models.Enums;
using System;
using System.Text.Json.Serialization;

namespace DiscordApiStuff.Models.Classes.Guild
{
    public sealed partial class DiscordGuild : Snowflake
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("splash")]
        public string Splash { get; set; }

        [JsonPropertyName("discovery_splash")]
        public string DiscoverySplash { get; set; }

        [JsonPropertyName("banner")]
        public string Banner { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("afk_timeout")]
        public int AfkTimeout { get; set; }

        [JsonPropertyName("joined_at")]
        public DateTime? JoinedAt { get; set; }

        [JsonPropertyName("large")]
        public bool? IsLarge { get; set; }

        [JsonPropertyName("unavailable")]
        public bool? Unavailable { get; set; }

        [JsonPropertyName("widget_enabled")]
        public bool? WidgetEnabled { get; set; }

        [JsonPropertyName("default_message_notifications")]
        public int DefaultMessageNotifications { get; set; }

        [JsonPropertyName("max_presences")]
        public int? MaxPresences { get; set; }

        [JsonPropertyName("max_members")]
        public int MaxMembers { get; set; }

        [JsonPropertyName("vanity_url_code")]
        public string VanityUrlCode { get; set; }

        [JsonPropertyName("premium_subscription_count")]
        public int PremiumSubscriptionCount { get; set; }

        [JsonPropertyName("system_channel_flags")]
        public int SystemChannelFlags { get; set; }

        [JsonPropertyName("preferred_locale")]
        public string PreferredLocale { get; set; }

        [JsonPropertyName("member_count")]
        public int? MemberCount { get; set; }
    }

    public sealed partial class DiscordGuild : Snowflake
    {
        [JsonPropertyName("premium_tier")]
        public PremiumTier PremiumTier { get; set; }

        [JsonPropertyName("verification_level")]
        public VerificationLevel VerificationLevel { get; set; }

        [JsonPropertyName("mfa_level")]
        public MfaLevel MfaLevel { get; set; }

        [JsonPropertyName("explicit_content_filter")]
        public ContentFilterLevel ExplicitContentFilter { get; set; }

    }
    public sealed partial class DiscordGuild : Snowflake
    {
        [JsonPropertyName("features")]
        public string[] Features { get; set; }

        [JsonPropertyName("emojis")]
        public Emoji[] Emojis { get; set; }

        [JsonPropertyName("roles")]
        public Role[] Roles { get; set; }

        [JsonPropertyName("voice_states")]
        public VoiceState[] VoiceStates { get; set; }

        [JsonPropertyName("members")]
        public GuildMember[] Members { get; set; }

        [JsonPropertyName("channels")]
        public GuildChannel[] Channels { get; set; }

        [JsonPropertyName("presences")]
        public Presence[] Presences { get; set; }
    }
    public sealed partial class DiscordGuild : Snowflake
    {
        [JsonPropertyName("owner_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong OwnerId { get; set; }

        [JsonPropertyName("application_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong? ApplicationId { get; set; }

        [JsonPropertyName("afk_channel_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong AfkChannelId { get; set; }

        [JsonPropertyName("system_channel_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong? SystemChannelId { get; set; }

        [JsonPropertyName("widget_channel_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong? WidgetChannelId { get; set; }

        [JsonPropertyName("rules_channel_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong RulesChannelId { get; set; }

        [JsonPropertyName("public_updates_channel_id")]
        [JsonConverter(typeof(SnowflakeConverter))]
        public ulong? PublicUpdatesChannelId { get; set; }
    }
}
