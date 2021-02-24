namespace DiscordApiStuff.Core
{
    internal sealed class DiscordApiInfo
    {
        //Easy configurable, easy to find
        public static readonly string DiscordApiGatewayVersion = "8";
        public static readonly string DiscordApiEncoding = "json";

        //Easy to interpolate
        public static readonly string DiscordWebSocketGateway_V8 = $"wss://gateway.discord.gg/?v={DiscordApiGatewayVersion}&encoding={DiscordApiEncoding}";
    }
}