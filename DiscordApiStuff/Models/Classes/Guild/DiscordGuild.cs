using DiscordApiStuff.Models.Classes.Channel;
using DiscordApiStuff.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DiscordApiStuff.Models.Classes.Guild
{
    public sealed class DiscordGuild : Snowflake
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

        [JsonPropertyName("features")]
        public List<string> Features { get; set; }

        [JsonPropertyName("emojis")]
        public List<Emoji> Emojis { get; set; }

        [JsonPropertyName("banner")]
        public string Banner { get; set; }

        [JsonPropertyName("owner_id")]
        public string OwnerId { get; set; }

        [JsonPropertyName("application_id")]
        public string ApplicationId { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("afk_channel_id")]
        public string AfkChannelId { get; set; }

        [JsonPropertyName("afk_timeout")]
        public int AfkTimeout { get; set; }

        [JsonPropertyName("system_channel_id")]
        public string SystemChannelId { get; set; }

        [JsonPropertyName("joined_at")]
        public DateTime? JoinedAt { get; set; }

        [JsonPropertyName("large")]
        public bool? IsLarge { get; set; }

        [JsonPropertyName("unavailable")]
        public bool? Unavailable { get; set; }

        [JsonPropertyName("widget_enabled")]
        public bool? WidgetEnabled { get; set; }

        [JsonPropertyName("widget_channel_id")]
        public string WidgetChannelId { get; set; }

        [JsonPropertyName("verification_level")]
        public VerificationLevel VerificationLevel { get; set; }

        [JsonPropertyName("roles")]
        public List<Role> Roles { get; set; }

        [JsonPropertyName("default_message_notifications")]
        public int DefaultMessageNotifications { get; set; }

        [JsonPropertyName("mfa_level")]
        public MfaLevel MfaLevel { get; set; }

        [JsonPropertyName("explicit_content_filter")]
        public ContentFilterLevel ExplicitContentFilter { get; set; }

        [JsonPropertyName("max_presences")]
        public int? MaxPresences { get; set; }

        [JsonPropertyName("max_members")]
        public int MaxMembers { get; set; }

        [JsonPropertyName("vanity_url_code")]
        public string VanityUrlCode { get; set; }

        [JsonPropertyName("premium_tier")]
        public PremiumTier PremiumTier { get; set; }

        [JsonPropertyName("premium_subscription_count")]
        public int PremiumSubscriptionCount { get; set; }

        [JsonPropertyName("system_channel_flags")]
        public int SystemChannelFlags { get; set; }

        [JsonPropertyName("preferred_locale")]
        public string PreferredLocale { get; set; }

        [JsonPropertyName("rules_channel_id")]
        public string RulesChannelId { get; set; }

        [JsonPropertyName("public_updates_channel_id")]
        public string PublicUpdatesChannelId { get; set; }

        [JsonPropertyName("member_count")]
        public int? MemberCount { get; set; }

        [JsonPropertyName("voice_states")]
        public IEnumerable<VoiceState> VoiceStates { get; set; }

        [JsonPropertyName("members")]
        public IEnumerable<GuildMember> Members { get; set; }

        [JsonPropertyName("channels")]
        public IEnumerable<GuildChannel> Channels { get; set; }

        [JsonPropertyName("presences")]
        public IEnumerable<Presence> Presences { get; set; }
    }
}
