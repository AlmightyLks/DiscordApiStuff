using System;
using System.Threading.Tasks;

namespace DiscordApiStuff
{
    class Program
    {
        static async Task Main()
        {
            DiscordClient client = new DiscordClient(new DiscordClientConfiguration()
            {
                Token = "ODEyNzU4MjYxNzkwNDA4NzI0.YDFaHQ.6YeNQbfOcqiS4Ns0dkCdpUJ1fhk",
                Intents = DiscordIntent.AllUnprivileged
            });
            await client.ConnectAsync();

            await Task.Delay(-1);
        }
    }

    public struct DiscordClientConfiguration
    {
        public string Token { get; init; }
        public DiscordIntent Intents { get; init; }
        public bool AutoReconnect { get; init; }
        public DiscordClientConfiguration(string token, DiscordIntent intents, bool autoReconnect)
        {
            Token = token;
            Intents = intents;
            AutoReconnect = autoReconnect;
        }
    }

    public enum ActivityType : byte
    {
        Playing,
        Streaming,
        Listening,
        //3 non-existent
        Custom = 4,
        Competing
    }
    public enum Opcode : byte
    {
        Dispatch,
        Heartbeat,
        Identify,
        PresenceUpdate,
        VoiceStateUpdate,
        //5 non-existent
        Resume = 6,
        Reconnect,
        RequestGuildMembers,
        InvalidSession,
        Hello,
        HeartbeatAck
    }

    public enum Nitro
    {
        None,
        NitroClassic,
        Nitro
    }

    [Flags]
    public enum UserFlags
    {
        None = 0 << 0,

        DiscordEmployee = 1 << 0,

        PartneredServerOwner = 1 << 1,

        HypeSquadEvents = 1 << 2,

        BugHunterLevel1 = 1 << 3,

        // 1 << 4 non-existent

        // 1 << 5 non-existent

        HouseBravery = 1 << 6,

        HouseBrilliance = 1 << 7,

        HouseBalance = 1 << 8,

        EarlySupporter = 1 << 9,

        TeamUser = 1 << 10,

        // 1 << 11 non-existent

        System = 1 << 12,

        // 1 << 13 non-existent

        BugHunterLevel2 = 1 << 14,

        // 1 << 15 non-existent

        VerifiedBot = 1 << 16,

        EarlyVerifiedBotDeveloper = 1 << 17
    }
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
    internal sealed class DiscordApiInfo
    {
        //Easy configurable, easy to find
        public static readonly string DiscordApiGatewayVersion = "8";
        public static readonly string DiscordApiEncoding = "json";

        //Easy to interpolate
        public static readonly string DiscordWebSocketGateway_V8 = $"wss://gateway.discord.gg/?v={DiscordApiGatewayVersion}&encoding={DiscordApiEncoding}";
    }
}