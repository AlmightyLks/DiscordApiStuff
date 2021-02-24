using System;

namespace DiscordApiStuff.Payloads.Models.Enums
{
    [Flags]
    public enum DiscordIntent
    {
        None = 0 << 0,

        Guilds = 1 << 0,

        GuildMembers = 1 << 1,

        GuildBan = 1 << 2,

        GuildEmojis = 1 << 3,

        GuildIntegrations = 1 << 4,

        GuildWebhooks = 1 << 5,

        GuildInvites = 1 << 6,

        GuildVoiceChat = 1 << 7,

        GuildPresence = 1 << 8,

        GuildMessages = 1 << 9,

        GuildReactions = 1 << 10,

        GuildTyping = 1 << 11,

        DirectMessages = 1 << 12,

        DirectMessageReaction = 1 << 13,

        DirectMessageTyping = 1 << 14,

        AllUnprivileged = Guilds | GuildBan | GuildEmojis | GuildIntegrations |
            GuildWebhooks | GuildInvites | GuildVoiceChat | GuildMessages |
            GuildReactions | GuildTyping | DirectMessages | DirectMessageReaction | DirectMessageTyping,

        All = AllUnprivileged | GuildMembers | GuildPresence
    }
}